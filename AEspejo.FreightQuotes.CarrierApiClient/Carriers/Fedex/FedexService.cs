using System.Net.Http.Headers;
using System.Text;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Response;
using AEspejo.FreightQuotes.CarrierApiClient.Http;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces.ICarriers;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex
{
    /// <summary>
    /// FedEx adapter over the OAuth and LTL freight rate endpoints. Builds the carrier-specific request
    /// (auth, headers, URL) and parses the reply; the shared transport is delegated to <see cref="IApiCaller"/>.
    /// </summary>
    public class FedexService(IApiCaller api) : IFedexService
    {
        public const string HttpClientName = "fedex";
        private const string AuthorizationHeader = "Authorization";

        private readonly IApiCaller _api = api;

        public async Task<FedexToken> Token(FedexTokenRequest tokenRequest, CancellationToken ct)
        {
            List<string> messages = [];

            var request = new HttpRequestMessage(HttpMethod.Post, tokenRequest.UrlToken)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials",
                    ["client_id"] = tokenRequest.ClientId,
                    ["client_secret"] = tokenRequest.ClientSecret,
                }),
            };

            var body = await _api.SendAsync(request, HttpClientName, tokenRequest.ApiCallTimeout, ct);
            var tokenResponse = _api.Deserialize<FedexTokenResponse>(body);

            if (tokenResponse?.Errors is { Length: > 0 })
            {
                messages.AddRange(tokenResponse.Errors.Select(e => e.Message));
            }
            else if (!string.IsNullOrWhiteSpace(tokenResponse?.AccessToken))
            {
                return new FedexToken { AccessToken = tokenResponse.AccessToken };
            }
            else
            {
                messages.Add("The login attempt was unsuccessful, try again please.");
            }

            return new FedexToken { Messages = messages };
        }

        public async Task<FedexRateResult> RateAsync(FedexRateRequest rateRequest, string accessToken,
            FedexCredentials credentials, CancellationToken ct)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, credentials.RateUrl)
            {
                Content = new StringContent(_api.SerializeJson(rateRequest), Encoding.UTF8, "application/json"),
            };
            request.Headers.TryAddWithoutValidation(AuthorizationHeader, $"Bearer {accessToken}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var body = await _api.SendAsync(request, HttpClientName, credentials.ApiCallTimeout, ct);

            return new FedexRateResult
            {
                Data = _api.Deserialize<FedexRateResponse>(body),
                RawResponse = body,
            };
        }
    }
}
