using AEspejo.FreightQuotes.CarrierApiClient.Common;
using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Response
{
    /// <summary>
    /// UPS OAuth token response. On success it carries <c>access_token</c> (from the base OAuth dto);
    /// on failure UPS returns an error envelope under <c>response.errors</c>.
    /// </summary>
    public class UpsTokenResponse : OauthTokenResponseDto
    {
        [JsonProperty("issued_at")]
        public string? IssuedAt { get; set; }

        [JsonProperty("client_id")]
        public string? ClientId { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("response")]
        public UpsErrorEnvelope? Response { get; set; }
    }
}
