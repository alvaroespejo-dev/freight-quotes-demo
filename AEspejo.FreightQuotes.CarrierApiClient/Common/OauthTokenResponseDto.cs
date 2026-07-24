using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.CarrierApiClient.Common
{
    /// <summary>
    /// Standard OAuth 2.0 client-credentials token response payload.
    /// </summary>
    public class OauthTokenResponseDto
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string? TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int? ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string? Scope { get; set; }
    }
}
