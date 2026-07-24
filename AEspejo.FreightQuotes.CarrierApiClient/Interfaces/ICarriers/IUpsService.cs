using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Response;

namespace AEspejo.FreightQuotes.CarrierApiClient.Interfaces.ICarriers
{
    public interface IUpsService
    {
        /// <summary>
        /// Requests a UPS OAuth access token (client-credentials flow, HTTP Basic auth).
        /// </summary>
        Task<UpsToken> Token(UpsTokenRequest tokenRequest, CancellationToken ct);

        /// <summary>
        /// Calls the UPS Rating endpoint (POST /rating/{version}/{requestoption}).
        /// </summary>
        Task<UpsRateResult> RateAsync(UpsRateRoot rateRequest, string accessToken, UpsCredentials credentials, CancellationToken ct);
    }

    /// <summary>
    /// Wraps a deserialized UPS rate response together with the raw payload (kept for logging/troubleshooting).
    /// </summary>
    public class UpsRateResult
    {
        public UpsRateResponseRoot? Data { get; set; }
        public string RawResponse { get; set; } = string.Empty;
    }
}
