namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Request
{
    /// <summary>
    /// Input needed to request a UPS OAuth token (client-credentials flow, HTTP Basic auth).
    /// </summary>
    public class UpsTokenRequest
    {
        public string UrlToken { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>Optional 6-digit UPS account number, sent as the <c>x-merchant-id</c> header.</summary>
        public string? MerchantId { get; set; }

        public int? ApiCallTimeout { get; set; }
    }

    /// <summary>
    /// Normalized token result surfaced to the rate client: either an access token or the error messages.
    /// </summary>
    public class UpsToken
    {
        public string? AccessToken { get; set; }
        public List<string> Messages { get; set; } = [];
    }
}
