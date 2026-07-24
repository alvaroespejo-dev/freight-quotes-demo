using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups
{
    /// <summary>
    /// UPS "code + description" pair used across request and response payloads
    /// (Service, PackagingType, UnitOfMeasurement, ...). Living in the parent namespace,
    /// it is visible to both the Request and Response child namespaces.
    /// </summary>
    public class UpsCodeDescription
    {
        [JsonProperty("Code")]
        public string? Code { get; set; }

        [JsonProperty("Description")]
        public string? Description { get; set; }
    }
}
