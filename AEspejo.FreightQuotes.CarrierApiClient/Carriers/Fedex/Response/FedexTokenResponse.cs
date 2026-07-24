using AEspejo.FreightQuotes.CarrierApiClient.Common;
using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Response
{
    public class FedexTokenResponse : OauthTokenResponseDto
    {
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; } = string.Empty;

        [JsonProperty("errors")]
        public Error[]? Errors { get; set; }
    }

    /// <summary>
    /// Partial that merges with the <c>Error</c> defined in the rate response, adding the
    /// code/message fields FedEx returns on auth and rating failures.
    /// </summary>
    public partial class Error
    {
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
