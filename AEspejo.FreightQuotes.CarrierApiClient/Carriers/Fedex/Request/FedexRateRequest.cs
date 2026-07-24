using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request
{
    public partial class FedexRateRequest
    {
        [JsonProperty("accountNumber")]
        public AccountNumber? AccountNumber { get; set; }

        [JsonProperty("version")]
        public FedexVersion? Version { get; set; }

        [JsonProperty("rateRequestControlParameters")]
        public RateRequestControlParameters? RateRequestControlParameters { get; set; }

        [JsonProperty("freightRequestedShipment")]
        public FreightRequestedShipment? FreightRequestedShipment { get; set; }
    }

    public partial class AccountNumber
    {
        [JsonProperty("value")]
        public string Value { get; set; } = string.Empty;
    }

    public partial class FreightRequestedShipment
    {
        [JsonProperty("shipper")]
        public FedexLocation? Shipper { get; set; }

        [JsonProperty("recipient")]
        public FedexLocation? Recipient { get; set; }

        [JsonProperty("serviceType")]
        public string ServiceType { get; set; } = string.Empty;

        [JsonProperty("preferredCurrency")]
        public string? PreferredCurrency { get; set; }

        [JsonProperty("shippingChargesPayment")]
        public ShippingChargesPayment? ShippingChargesPayment { get; set; }

        [JsonProperty("rateRequestType")]
        public string[]? RateRequestType { get; set; }

        [JsonProperty("shipDateStamp")]
        public string ShipDateStamp { get; set; } = string.Empty;

        [JsonProperty("requestedPackageLineItems")]
        public RequestedPackageLineItem[]? RequestedPackageLineItems { get; set; }

        [JsonProperty("totalPackageCount")]
        public long? TotalPackageCount { get; set; }

        [JsonProperty("totalWeight")]
        public long TotalWeight { get; set; }

        [JsonProperty("freightShipmentDetail")]
        public FreightShipmentDetail? FreightShipmentDetail { get; set; }

        [JsonProperty("freightShipmentSpecialServices")]
        public FreightShipmentSpecialServices? FreightShipmentSpecialServices { get; set; }
    }

    public partial class FreightShipmentDetail
    {
        [JsonProperty("role")]
        public string Role { get; set; } = string.Empty;

        [JsonProperty("accountNumber")]
        public AccountNumber? AccountNumber { get; set; }

        [JsonProperty("fedExFreightBillingContactAndAddress")]
        public FedExFreightBillingContactAndAddress? FedExFreightBillingContactAndAddress { get; set; }

        [JsonProperty("lineItem")]
        public LineItem[]? LineItem { get; set; }

        [JsonProperty("alternateBillingParty")]
        public AlternateBillingParty? AlternateBillingParty { get; set; }
    }

    public partial class AlternateBillingParty
    {
        [JsonProperty("address")]
        public BillingAddress? Address { get; set; }

        [JsonProperty("accountNumber")]
        public AccountNumber? AccountNumber { get; set; }

        [JsonProperty("contact")]
        public Contact? Contact { get; set; }
    }

    public partial class BillingAddress
    {
        [JsonProperty("streetLines")]
        public string[]? StreetLines { get; set; }

        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;

        [JsonProperty("stateOrProvinceCode")]
        public string StateOrProvinceCode { get; set; } = string.Empty;

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// US, CA, MX
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; } = string.Empty;
    }

    public partial class Address
    {
        [JsonProperty("streetLines")]
        public string[]? StreetLines { get; set; }

        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;

        [JsonProperty("stateOrProvinceCode")]
        public string StateOrProvinceCode { get; set; } = string.Empty;

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; } = string.Empty;

        /// <summary>
        /// US, CA, MX
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; } = string.Empty;

        [JsonProperty("residential")]
        public bool Residential { get; set; }
    }

    public partial class DeclaredValue
    {
        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = string.Empty;
    }

    public partial class FedExFreightBillingContactAndAddress
    {
        [JsonProperty("address")]
        public Address? Address { get; set; }

        [JsonProperty("contact")]
        public Contact? Contact { get; set; }
    }

    public partial class Contact
    {
        [JsonProperty("personName")]
        public string? PersonName { get; set; }

        [JsonProperty("emailAddress")]
        public string? EmailAddress { get; set; }

        [JsonProperty("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("phoneExtension")]
        public string? PhoneExtension { get; set; }

        [JsonProperty("companyName")]
        public string? CompanyName { get; set; }

        [JsonProperty("faxNumber")]
        public string? FaxNumber { get; set; }
    }

    public partial class LineItem
    {
        [JsonProperty("handlingUnits")]
        public long HandlingUnits { get; set; }

        [JsonProperty("nmfcCode")]
        public string? NmfcCode { get; set; }

        [JsonProperty("subPackagingType")]
        public string SubPackagingType { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("weight")]
        public RateWeight Weight { get; set; } = new();

        [JsonProperty("pieces")]
        public long Pieces { get; set; }

        [JsonProperty("volume")]
        public RateVolume? Volume { get; set; }

        [JsonProperty("freightClass")]
        public string FreightClass { get; set; } = string.Empty;

        [JsonProperty("purchaseOrderNumber")]
        public string? PurchaseOrderNumber { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("hazardousMaterials")]
        public string? HazardousMaterials { get; set; }

        [JsonProperty("dimensions")]
        public RateDimensions? Dimensions { get; set; }
    }

    public partial class RateDimensions
    {
        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; } = string.Empty;
    }

    public partial class RateWeight
    {
        [JsonProperty("units")]
        public string Units { get; set; } = string.Empty;

        [JsonProperty("value")]
        public decimal Value { get; set; }
    }

    public partial class RateVolume
    {
        [JsonProperty("units")]
        public string Units { get; set; } = string.Empty;

        [JsonProperty("value")]
        public decimal Value { get; set; }
    }

    public partial class FreightShipmentSpecialServices
    {
        [JsonProperty("freightGuaranteeDetail")]
        public FreightGuaranteeDetail? FreightGuaranteeDetail { get; set; }

        [JsonProperty("specialServiceTypes")]
        public string[]? SpecialServiceTypes { get; set; }

        [JsonProperty("freightDirectDetail")]
        public FreightDirectDetail? FreightDirectDetail { get; set; }
    }

    public partial class FreightDirectDetail
    {
        [JsonProperty("freightDirectDataDetails")]
        public FreightDirectDataDetail[]? FreightDirectDataDetails { get; set; }
    }

    public partial class FreightDirectDataDetail
    {
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("transportationType")]
        public string TransportationType { get; set; } = string.Empty;

        [JsonProperty("emailAddress")]
        public string? EmailAddress { get; set; }

        [JsonProperty("phoneNumberDetails")]
        public PhoneNumberDetail[]? PhoneNumberDetails { get; set; }
    }

    public partial class PhoneNumberDetail
    {
        [JsonProperty("phoneNumberType")]
        public string PhoneNumberType { get; set; } = string.Empty;

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public partial class FreightGuaranteeDetail
    {
        [JsonProperty("freightGuaranteeType")]
        public string FreightGuaranteeType { get; set; } = string.Empty;

        [JsonProperty("guaranteeTimestamp")]
        public string GuaranteeTimestamp { get; set; } = string.Empty;
    }

    public partial class FedexLocation
    {
        [JsonProperty("address")]
        public Address? Address { get; set; }
    }

    public partial class RequestedPackageLineItem
    {
        [JsonProperty("subPackagingType")]
        public string SubPackagingType { get; set; } = string.Empty;

        [JsonProperty("contentRecord")]
        public ContentRecord[]? ContentRecord { get; set; }

        [JsonProperty("declaredValue")]
        public DeclaredValue? DeclaredValue { get; set; }

        [JsonProperty("weight")]
        public RateWeight? Weight { get; set; }

        [JsonProperty("dimensions")]
        public RateDimensions? Dimensions { get; set; }

        [JsonProperty("associatedFreightLineItems")]
        public AssociatedFreightLineItem[]? AssociatedFreightLineItems { get; set; }
    }

    public partial class AssociatedFreightLineItem
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
    }

    public partial class ContentRecord
    {
        [JsonProperty("itemNumber")]
        public string ItemNumber { get; set; } = string.Empty;

        [JsonProperty("receivedQuantity")]
        public long ReceivedQuantity { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("partNumber")]
        public string PartNumber { get; set; } = string.Empty;
    }

    public partial class ShippingChargesPayment
    {
        [JsonProperty("payor")]
        public Payor? Payor { get; set; }

        [JsonProperty("paymentType")]
        public string PaymentType { get; set; } = string.Empty;
    }

    public partial class Payor
    {
        [JsonProperty("responsibleParty")]
        public ResponsibleParty? ResponsibleParty { get; set; }
    }

    public partial class ResponsibleParty
    {
        [JsonProperty("address")]
        public BillingAddress? Address { get; set; }

        [JsonProperty("contact")]
        public Contact? Contact { get; set; }

        [JsonProperty("accountNumber")]
        public AccountNumber? AccountNumber { get; set; }
    }

    public partial class RateRequestControlParameters
    {
        [JsonProperty("returnTransitTimes")]
        public bool ReturnTransitTimes { get; set; }

        [JsonProperty("servicesNeededOnRateFailure")]
        public bool ServicesNeededOnRateFailure { get; set; }

        [JsonProperty("variableOptions")]
        public string? VariableOptions { get; set; }

        [JsonProperty("rateSortOrder")]
        public string? RateSortOrder { get; set; }
    }

    public partial class FedexVersion
    {
        [JsonProperty("major")]
        public int? Major { get; set; }

        [JsonProperty("minor")]
        public int? Minor { get; set; }

        [JsonProperty("patch")]
        public int? Patch { get; set; }
    }

    /// <summary>
    /// Specify service options whose combinations are to be considered when replying with available services.
    /// </summary>
    public enum VariableOptions
    {
        SATURDAY_DELIVERY,
        FREIGHT_GUARANTEE
    }

    /// <summary>
    /// This is a sort order you can specify to control the order of the response data:
    /// </summary>
    public enum RateSortOrder
    {
        SERVICENAMETRADITIONAL, // data in order of highest to lowest service (Default)
        COMMITASCENDING, // data in order of ascending delivery committment
        COMMITDESCENDING, // data in order of descending delivery committment.
    }

    public enum FedexServiceType
    {
        FEDEX_FREIGHT_ECONOMY,
        FEDEX_FREIGHT_PRIORITY,
    }

    public enum FedexRateRequestType
    {
        LIST, // Returns FedEx published list rates in addition to account-specific rates (if applicable).
        INCENTIVE, // This is one-time discount for incentivising the customer. For more information, contact your FedEx representative.
        ACCOUNT, // Returns account specific rates (Default).
    }

    public enum SubPackageTypes
    {
        BAG,
        BARREL,
        BASKET,
        BOX,
        BUCKET,
        BUNDLE,
        CAGE,
        CARTON,
        CASE,
        CHEST,
        CONTAINER,
        CRATE,
        CYLINDER,
        DRUM,
        ENVELOPE,
        HAMPER,
        OTHER,
        PACKAGE,
        PAIL,
        PALLET,
        PARCEL,
        PIECE,
        REEL,
        ROLL,
        SACK,
        SHRINKWRAPPED,
        SKID,
        TANK,
        TOTEBIN,
        TUBE,
        UNIT,
    }

    public enum PackageWeightUnit
    {
        KG,
        LB
    }

    public enum UnitDimensions
    {
        IN, // Inches
        CM, // Centimeters
    }

    public enum FedexRole
    {
        SHIPPER,
        CONSIGNEE,
    }

    public enum FedexFreightClass
    {
        CLASS_050,
        CLASS_055,
        CLASS_060,
        CLASS_065,
        CLASS_070,
        CLASS_077_5,
        CLASS_085,
        CLASS_092_5,
        CLASS_100,
        CLASS_110,
        CLASS_125,
        CLASS_150,
        CLASS_175,
        CLASS_200,
        CLASS_250,
        CLASS_300,
        CLASS_400,
        CLASS_500
    }

    public enum FreightDirectDataDetailType
    {
        BASIC,
        BASIC_BY_APPOINTMENT,
        PREMIUM,
        STANDARD,
    }

    public enum TransportationType
    {
        DELIVERY,
        PICKUP
    }

    public enum SpecialServiceTypes
    {
        BROKER_SELECT_OPTION,
        CALL_BEFORE_DELIVERY,
        COD,
        CUSTOM_DELIVERY_WINDOW,
        DANGEROUS_GOODS,
        DO_NOT_BREAK_DOWN_PALLETS,
        DO_NOT_STACK_PALLETS,
        EXTREME_LENGTH,
        FOOD,
        FREIGHT_DIRECT,
        FREIGHT_GUARANTEE,
        INSIDE_DELIVERY,
        INSIDE_PICKUP,
        LIFTGATE_DELIVERY,
        LIFTGATE_PICKUP,
        LIMITED_ACCESS_DELIVERY,
        LIMITED_ACCESS_PICKUP,
        OVER_LENGTH,
        POISON,
        PROTECTION_FROM_FREEZING,
        TOP_LOAD,
    }

    public enum PaymentType
    {
        SENDER,
        RECIPIENT,
        THIRD_PARTY,
    }
}
