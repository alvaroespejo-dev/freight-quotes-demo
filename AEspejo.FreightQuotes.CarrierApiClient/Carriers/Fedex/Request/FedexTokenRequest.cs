namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request
{
    /// <summary>
    /// Input needed to request a FedEx OAuth token (client-credentials flow).
    /// </summary>
    public class FedexTokenRequest
    {
        public string UrlToken { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public int? ApiCallTimeout { get; set; }
    }

    /// <summary>
    /// Normalized token result surfaced to the rate client: either an access token or the error messages.
    /// </summary>
    public class FedexToken
    {
        public string? AccessToken { get; set; }
        public List<string> Messages { get; set; } = [];
    }
}
