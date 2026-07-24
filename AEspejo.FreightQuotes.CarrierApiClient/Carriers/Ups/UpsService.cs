using System.Net.Http.Headers;
using System.Text;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Constants;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Response;
using AEspejo.FreightQuotes.CarrierApiClient.Http;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces.ICarriers;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups
{
    /// <summary>
    /// UPS adapter over the OAuth (Basic-auth client-credentials) and Rating endpoints. Builds the carrier-specific
    /// request (auth, headers, URL) and parses the reply; the shared transport is delegated to <see cref="IApiCaller"/>.
    /// </summary>
    public class UpsService(IApiCaller api) : IUpsService
    {
        public const string HttpClientName = "ups";
        private const string AuthorizationHeader = "Authorization";
        private const string MerchantIdHeader = "x-merchant-id";
        private const string TransIdHeader = "transId";
        private const string TransactionSrcHeader = "transactionSrc";

        private readonly IApiCaller _api = api;

        public async Task<UpsToken> Token(UpsTokenRequest tokenRequest, CancellationToken ct)
        {
            List<string> messages = [];

            var request = new HttpRequestMessage(HttpMethod.Post, tokenRequest.UrlToken)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials",
                }),
            };

            // UPS OAuth authenticates via HTTP Basic (base64 of clientId:clientSecret), unlike FedEx's form body.
            var basic = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{tokenRequest.ClientId}:{tokenRequest.ClientSecret}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", basic);
            if (!string.IsNullOrWhiteSpace(tokenRequest.MerchantId))
            {
                request.Headers.TryAddWithoutValidation(MerchantIdHeader, tokenRequest.MerchantId);
            }

            var body = await _api.SendAsync(request, HttpClientName, tokenRequest.ApiCallTimeout, ct);
            var tokenResponse = _api.Deserialize<UpsTokenResponse>(body);

            if (!string.IsNullOrWhiteSpace(tokenResponse?.AccessToken))
            {
                return new UpsToken { AccessToken = tokenResponse.AccessToken };
            }

            if (tokenResponse?.Response?.Errors is { Length: > 0 })
            {
                messages.AddRange(tokenResponse.Response.Errors.Select(e => e.Message));
            }
            else
            {
                messages.Add("The login attempt was unsuccessful, try again please.");
            }

            return new UpsToken { Messages = messages };
        }

        public async Task<UpsRateResult> RateAsync(UpsRateRoot rateRequest, string accessToken,
            UpsCredentials credentials, CancellationToken ct)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BuildRateUrl(credentials.RateUrl))
            {
                Content = new StringContent(_api.SerializeJson(rateRequest), Encoding.UTF8, "application/json"),
            };
            request.Headers.TryAddWithoutValidation(AuthorizationHeader, $"Bearer {accessToken}");
            request.Headers.TryAddWithoutValidation(TransIdHeader, Guid.NewGuid().ToString("N"));
            request.Headers.TryAddWithoutValidation(TransactionSrcHeader, UpsApiConstants.TransactionSource);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var body = await _api.SendAsync(request, HttpClientName, credentials.ApiCallTimeout, ct);

            return new UpsRateResult
            {
                Data = _api.Deserialize<UpsRateResponseRoot>(body),
                RawResponse = body,
            };
        }

        /// <summary>
        /// Appends the version and request option to the configured base Rating URL:
        /// <c>{RateUrl}/{version}/{requestoption}</c>.
        /// </summary>
        private static string BuildRateUrl(string baseUrl)
            => $"{baseUrl.TrimEnd('/')}/{UpsApiConstants.RatingVersion}/{UpsApiConstants.RequestOptionRate}";
    }
}
