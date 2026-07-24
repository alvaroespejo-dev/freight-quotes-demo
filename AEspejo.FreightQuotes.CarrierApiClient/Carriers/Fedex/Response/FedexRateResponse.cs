using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request;
using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Response
{
    public partial class FedexRateResponse
    {
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; } = string.Empty;

        [JsonProperty("customerTransactionId")]
        public string CustomerTransactionId { get; set; } = string.Empty;

        [JsonProperty("output")]
        public RatesOutput? Output { get; set; }

        [JsonProperty("errors")]
        public Error[]? Errors { get; set; }
    }

    public partial class RatesOutput
    {
        [JsonProperty("customerTransactionId")]
        public string? CustomerTransactionId { get; set; }

        [JsonProperty("alerts")]
        public RateAlert[]? Alerts { get; set; }

        [JsonProperty("rateReplyDetails")]
        public RateReplyDetail[]? RateReplyDetails { get; set; }

        [JsonProperty("quoteDate")]
        public DateTime? QuoteDate { get; set; }

        [JsonProperty("encoded")]
        public bool? Encoded { get; set; }
    }

    public partial class RateAlert
    {
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("alertType")]
        public string AlertType { get; set; } = string.Empty;

        [JsonProperty("parameterList")]
        public RateParameterList[]? ParameterList { get; set; }
    }

    public partial class RateParameterList
    {
        [JsonProperty("key")]
        public string Key { get; set; } = string.Empty;

        [JsonProperty("value")]
        public string? Value { get; set; }
    }

    public partial class RateReplyDetail
    {
        [JsonProperty("serviceType")]
        public string ServiceType { get; set; } = string.Empty;

        [JsonProperty("serviceName")]
        public string? ServiceName { get; set; }

        [JsonProperty("packagingType")]
        public string? PackagingType { get; set; }

        [JsonProperty("customerMessages")]
        public RateCustomerMessage[]? CustomerMessages { get; set; }

        [JsonProperty("commit")]
        public Commit? Commit { get; set; }

        [JsonProperty("ratedShipmentDetails")]
        public RatedShipmentDetail[]? RatedShipmentDetails { get; set; }

        [JsonProperty("operationalDetail")]
        public OperationalDetail? OperationalDetail { get; set; }

        [JsonProperty("freightTransitLocationDetail")]
        public FreightTransitLocationDetail? FreightTransitLocationDetail { get; set; }

        [JsonProperty("signatureOptionType")]
        public string? SignatureOptionType { get; set; }

        [JsonProperty("serviceDescription")]
        public RateServiceDescription? ServiceDescription { get; set; }

        [JsonProperty("brokerDetail")]
        public RateBrokerDetail? BrokerDetail { get; set; }
    }

    public partial class RateCustomerMessage
    {
        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }
    }

    public partial class Commit
    {
        [JsonProperty("dateDetail")]
        public DateDetail? DateDetail { get; set; }

        [JsonProperty("transitDays")]
        public TransitDays? TransitDays { get; set; }

        [JsonProperty("saturdayDelivery")]
        public bool? SaturdayDelivery { get; set; }
    }

    public partial class DateDetail
    {
        [JsonProperty("dayOfWeek")]
        public string DayOfWeek { get; set; } = string.Empty;

        [JsonProperty("dayFormat")]
        public DateTime? DayFormat { get; set; }
    }

    public partial class TransitDays
    {
        [JsonProperty("minimumTransitTime")]
        public string MinimumTransitTime { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;
    }

    public partial class FreightTransitLocationDetail
    {
        [JsonProperty("originLocation")]
        public NLocation? OriginLocation { get; set; }

        [JsonProperty("destinationLocation")]
        public NLocation? DestinationLocation { get; set; }

        [JsonProperty("distance")]
        public RateDistance? Distance { get; set; }
    }

    public partial class NLocation
    {
        [JsonProperty("contactAndAddress")]
        public ContactAndAddress? ContactAndAddress { get; set; }

        [JsonProperty("locationId")]
        public string LocationId { get; set; } = string.Empty;
    }

    public partial class ContactAndAddress
    {
        [JsonProperty("contact")]
        public Contact? Contact { get; set; }

        [JsonProperty("address")]
        public Address? Address { get; set; }
    }

    public partial class RateDistance
    {
        [JsonProperty("units")]
        public string Units { get; set; } = string.Empty;

        [JsonProperty("value")]
        public decimal? Value { get; set; }
    }

    /// <summary>
    /// Weight measure reported by FedEx (e.g. total billing weight / dim weight).
    /// Defined here because the FedEx rate response references it.
    /// </summary>
    public partial class Distance
    {
        [JsonProperty("units")]
        public string Units { get; set; } = string.Empty;

        [JsonProperty("value")]
        public decimal? Value { get; set; }
    }

    public partial class OperationalDetail
    {
        [JsonProperty("originLocationIds")]
        public string[]? OriginLocationIds { get; set; }

        [JsonProperty("originLocationNumbers")]
        public int[]? OriginLocationNumbers { get; set; }

        [JsonProperty("serviceCode")]
        public string? ServiceCode { get; set; }

        [JsonProperty("airportId")]
        public string? AirportId { get; set; }

        [JsonProperty("scac")]
        public string? Scac { get; set; }

        [JsonProperty("originServiceAreas")]
        public string[]? OriginServiceAreas { get; set; }

        [JsonProperty("deliveryDate")]
        public DateTime? DeliveryDate { get; set; }

        [JsonProperty("deliveryDay")]
        public string DeliveryDay { get; set; } = string.Empty;

        [JsonProperty("commitDate")]
        public DateTime? CommitDate { get; set; }

        [JsonProperty("commitDays")]
        public string[]? CommitDays { get; set; }

        [JsonProperty("destinationPostalCode")]
        public string? DestinationPostalCode { get; set; }

        [JsonProperty("astraDescription")]
        public string? AstraDescription { get; set; }

        [JsonProperty("deliveryEligibilities")]
        public string[]? DeliveryEligibilities { get; set; }

        [JsonProperty("transitTime")]
        public string TransitTime { get; set; } = string.Empty;

        [JsonProperty("ineligibleForMoneyBackGuarantee")]
        public bool? IneligibleForMoneyBackGuarantee { get; set; }

        [JsonProperty("MaximumTransitTime")]
        public string? MaximumTransitTime { get; set; }

        [JsonProperty("astraPlannedServiceLevel")]
        public string? AstraPlannedServiceLevel { get; set; }

        [JsonProperty("destinationLocationIds")]
        public string[]? DestinationLocationIds { get; set; }

        [JsonProperty("destinationLocationStateOrProvinceCodes")]
        public string[]? DestinationLocationStateOrProvinceCodes { get; set; }

        [JsonProperty("packagingCode")]
        public string? PackagingCode { get; set; }

        [JsonProperty("destinationLocationNumbers")]
        public int[]? DestinationLocationNumbers { get; set; }

        [JsonProperty("publishedDeliveryTime")]
        public string? PublishedDeliveryTime { get; set; }

        [JsonProperty("countryCodes")]
        public string[]? CountryCodes { get; set; }

        [JsonProperty("stateOrProvinceCodes")]
        public string[]? StateOrProvinceCodes { get; set; }

        [JsonProperty("ursaPrefixCode")]
        public string? UrsaPrefixCode { get; set; }

        [JsonProperty("ursaSuffixCode")]
        public string? UrsaSuffixCode { get; set; }

        [JsonProperty("destinationServiceAreas")]
        public string[]? DestinationServiceAreas { get; set; }

        [JsonProperty("originPostalCodes")]
        public string[]? OriginPostalCodes { get; set; }

        [JsonProperty("customTransitTime")]
        public string? CustomTransitTime { get; set; }
    }

    public partial class RatedShipmentDetail
    {
        [JsonProperty("quoteNumber")]
        public string? QuoteNumber { get; set; }

        [JsonProperty("rateType")]
        public string RateType { get; set; } = string.Empty;

        [JsonProperty("freightChargeBasis")]
        public string FreightChargeBasis { get; set; } = string.Empty;

        [JsonProperty("ratedWeightMethod")]
        public string RatedWeightMethod { get; set; } = string.Empty;

        [JsonProperty("totalDiscounts")]
        public decimal? TotalDiscounts { get; set; }

        [JsonProperty("totalBaseCharge")]
        public decimal? TotalBaseCharge { get; set; }

        [JsonProperty("totalNetCharge")]
        public decimal? TotalNetCharge { get; set; }

        [JsonProperty("totalNetFedExCharge")]
        public decimal? TotalNetFedExCharge { get; set; }

        [JsonProperty("shipmentLegRateDetails")]
        public ShipmentLegRateDetail[]? ShipmentLegRateDetails { get; set; }

        [JsonProperty("shipmentRateDetail")]
        public ShipmentRateDetail? ShipmentRateDetail { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = string.Empty;
    }

    public partial class ShipmentLegRateDetail
    {
        [JsonProperty("rateScale")]
        public string RateScale { get; set; } = string.Empty;

        [JsonProperty("totalBaseCharge")]
        public decimal? TotalBaseCharge { get; set; }

        [JsonProperty("totalNetCharge")]
        public decimal? TotalNetCharge { get; set; }

        [JsonProperty("totalBillingWeight")]
        public Distance? TotalBillingWeight { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; }
    }

    public partial class ShipmentRateDetail
    {
        [JsonProperty("currencyExchangeRate")]
        public RateCurrencyExchangeRate? CurrencyExchangeRate { get; set; }

        [JsonProperty("dimDivisor")]
        public long? DimDivisor { get; set; }

        [JsonProperty("fuelSurchargePercent")]
        public decimal? FuelSurchargePercent { get; set; }

        [JsonProperty("totalSurcharges")]
        public decimal? TotalSurcharges { get; set; }

        [JsonProperty("totalFreightDiscount")]
        public decimal? TotalFreightDiscount { get; set; }

        [JsonProperty("surCharges")]
        public SurCharge[]? SurCharges { get; set; }

        [JsonProperty("totalBillingWeight")]
        public Distance? TotalBillingWeight { get; set; }

        [JsonProperty("totalDimWeight")]
        public Distance? TotalDimWeight { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = string.Empty;

        [JsonProperty("rateZone")]
        public string? RateZone { get; set; }

        [JsonProperty("pricingCode")]
        public string? PricingCode { get; set; }

        [JsonProperty("specialRatingApplied")]
        public string[]? SpecialRatingApplied { get; set; }

        [JsonProperty("freightDiscount")]
        public RateDiscount[]? FreightDiscount { get; set; }

        [JsonProperty("rateScale")]
        public string RateScale { get; set; } = string.Empty;
    }

    public partial class SurCharge
    {
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("level")]
        public string Level { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }
    }

    public partial class RateCurrencyExchangeRate
    {
        [JsonProperty("fromCurrency")]
        public string? FromCurrency { get; set; }

        [JsonProperty("intoCurrency")]
        public string? IntoCurrency { get; set; }

        [JsonProperty("rate")]
        public decimal? Rate { get; set; }
    }

    public partial class RateDiscount
    {
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("percent")]
        public decimal? Percent { get; set; }
    }

    public partial class RateServiceDescription
    {
        [JsonProperty("serviceType")]
        public string? ServiceType { get; set; }

        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("names")]
        public RateProductName[]? Names { get; set; }

        [JsonProperty("operatingOrgCodes")]
        public string[]? OperatingOrgCodes { get; set; }

        [JsonProperty("astraDescription")]
        public string? AstraDescription { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("serviceId")]
        public string? ServiceId { get; set; }

        [JsonProperty("serviceCategory")]
        public string? ServiceCategory { get; set; }
    }

    public partial class RateProductName
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("encoding")]
        public string? Encoding { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }
    }

    public partial class RateBrokerDetail
    {
        [JsonProperty("broker")]
        public ResponsibleParty? Broker { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("brokerCommitTimestamp")]
        public string? BrokerCommitTimestamp { get; set; }

        [JsonProperty("brokerCommitDayOfWeek")]
        public string? BrokerCommitDayOfWeek { get; set; }

        [JsonProperty("brokerLocationId")]
        public string? BrokerLocationId { get; set; }

        [JsonProperty("brokerAddress")]
        public Address? BrokerAddress { get; set; }

        [JsonProperty("brokerToDestinationDays")]
        public int? BrokerToDestinationDays { get; set; }
    }

    public partial class ErrorParameter
    {
        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }
    }

    public partial class Error
    {
        [JsonProperty("parameterList")]
        public ErrorParameter[]? ParameterList { get; set; }
    }

    public class FedexRateErrorResponse
    {
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; } = string.Empty;

        [JsonProperty("errors")]
        public Error[]? Errors { get; set; }
    }
}
