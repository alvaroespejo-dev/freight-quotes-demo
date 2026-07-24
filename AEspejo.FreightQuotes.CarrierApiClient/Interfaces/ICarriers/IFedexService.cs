using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Response;

namespace AEspejo.FreightQuotes.CarrierApiClient.Interfaces.ICarriers
{
    public interface IFedexService
    {
        /// <summary>
        /// Requests a FedEx OAuth access token (client-credentials flow).
        /// </summary>
        Task<FedexToken> Token(FedexTokenRequest tokenRequest, CancellationToken ct);

        /// <summary>
        /// Calls the FedEx LTL freight rate endpoint.
        /// </summary>
        Task<FedexRateResult> RateAsync(FedexRateRequest rateRequest, string accessToken, FedexCredentials credentials, CancellationToken ct);
    }

    /// <summary>
    /// Wraps a deserialized FedEx rate response together with the raw payload (kept for logging/troubleshooting).
    /// </summary>
    public class FedexRateResult
    {
        public FedexRateResponse? Data { get; set; }
        public string RawResponse { get; set; } = string.Empty;
    }
}
