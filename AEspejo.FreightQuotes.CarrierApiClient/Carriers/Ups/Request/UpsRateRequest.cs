using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Request
{
    /// <summary>
    /// UPS Rating API request body. Property names/casing match the documented JSON schema
    /// (POST /rating/{version}/{requestoption}). Null members are dropped at serialization time.
    /// </summary>
    public class UpsRateRoot
    {
        [JsonProperty("RateRequest")]
        public UpsRateRequest RateRequest { get; set; } = new();
    }

    public class UpsRateRequest
    {
        [JsonProperty("Request")]
        public UpsRequest Request { get; set; } = new();

        [JsonProperty("Shipment")]
        public UpsShipment Shipment { get; set; } = new();
    }

    public class UpsRequest
    {
        [JsonProperty("TransactionReference")]
        public UpsTransactionReference? TransactionReference { get; set; }
    }

    public class UpsTransactionReference
    {
        [JsonProperty("CustomerContext")]
        public string? CustomerContext { get; set; }
    }

    public class UpsShipment
    {
        [JsonProperty("Shipper")]
        public UpsShipper Shipper { get; set; } = new();

        [JsonProperty("ShipTo")]
        public UpsAddressParty ShipTo { get; set; } = new();

        [JsonProperty("ShipFrom")]
        public UpsAddressParty ShipFrom { get; set; } = new();

        [JsonProperty("PaymentDetails")]
        public UpsPaymentDetails? PaymentDetails { get; set; }

        [JsonProperty("Service")]
        public UpsCodeDescription? Service { get; set; }

        [JsonProperty("NumOfPieces")]
        public string? NumOfPieces { get; set; }

        [JsonProperty("Package")]
        public List<UpsPackage> Package { get; set; } = [];
    }

    public class UpsShipper
    {
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonProperty("ShipperNumber")]
        public string? ShipperNumber { get; set; }

        [JsonProperty("Address")]
        public UpsAddress Address { get; set; } = new();
    }

    public class UpsAddressParty
    {
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [JsonProperty("Address")]
        public UpsAddress Address { get; set; } = new();
    }

    public class UpsAddress
    {
        [JsonProperty("AddressLine")]
        public List<string> AddressLine { get; set; } = [];

        [JsonProperty("City")]
        public string? City { get; set; }

        [JsonProperty("StateProvinceCode")]
        public string? StateProvinceCode { get; set; }

        [JsonProperty("PostalCode")]
        public string? PostalCode { get; set; }

        [JsonProperty("CountryCode")]
        public string? CountryCode { get; set; }
    }

    public class UpsPaymentDetails
    {
        [JsonProperty("ShipmentCharge")]
        public List<UpsShipmentCharge> ShipmentCharge { get; set; } = [];
    }

    public class UpsShipmentCharge
    {
        [JsonProperty("Type")]
        public string? Type { get; set; }

        [JsonProperty("BillShipper")]
        public UpsBillShipper? BillShipper { get; set; }
    }

    public class UpsBillShipper
    {
        [JsonProperty("AccountNumber")]
        public string? AccountNumber { get; set; }
    }

    public class UpsPackage
    {
        [JsonProperty("PackagingType")]
        public UpsCodeDescription? PackagingType { get; set; }

        [JsonProperty("Dimensions")]
        public UpsDimensions? Dimensions { get; set; }

        [JsonProperty("PackageWeight")]
        public UpsPackageWeight? PackageWeight { get; set; }
    }

    public class UpsDimensions
    {
        [JsonProperty("UnitOfMeasurement")]
        public UpsCodeDescription? UnitOfMeasurement { get; set; }

        [JsonProperty("Length")]
        public string? Length { get; set; }

        [JsonProperty("Width")]
        public string? Width { get; set; }

        [JsonProperty("Height")]
        public string? Height { get; set; }
    }

    public class UpsPackageWeight
    {
        [JsonProperty("UnitOfMeasurement")]
        public UpsCodeDescription? UnitOfMeasurement { get; set; }

        [JsonProperty("Weight")]
        public string? Weight { get; set; }
    }
}
