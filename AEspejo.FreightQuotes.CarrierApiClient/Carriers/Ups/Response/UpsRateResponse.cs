using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Response
{
    /// <summary>
    /// Root of a UPS Rating reply. A success body carries <see cref="RateResponse"/>;
    /// an error body (HTTP 4xx) carries the <see cref="ErrorEnvelope"/> under <c>response.errors</c>.
    /// </summary>
    public class UpsRateResponseRoot
    {
        [JsonProperty("RateResponse")]
        public UpsRateResponse? RateResponse { get; set; }

        [JsonProperty("response")]
        public UpsErrorEnvelope? ErrorEnvelope { get; set; }
    }

    public class UpsErrorEnvelope
    {
        [JsonProperty("errors")]
        public UpsError[]? Errors { get; set; }
    }

    public class UpsError
    {
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }

    public class UpsRateResponse
    {
        [JsonProperty("Response")]
        public UpsResponseStatus? Response { get; set; }

        [JsonProperty("RatedShipment")]
        public UpsRatedShipment[]? RatedShipment { get; set; }
    }

    public class UpsResponseStatus
    {
        [JsonProperty("Alert")]
        public UpsCodeDescription[]? Alert { get; set; }
    }

    public class UpsRatedShipment
    {
        [JsonProperty("Service")]
        public UpsCodeDescription? Service { get; set; }

        [JsonProperty("BillingWeight")]
        public UpsBillingWeight? BillingWeight { get; set; }

        [JsonProperty("TransportationCharges")]
        public UpsMoney? TransportationCharges { get; set; }

        [JsonProperty("BaseServiceCharge")]
        public UpsMoney? BaseServiceCharge { get; set; }

        [JsonProperty("ServiceOptionsCharges")]
        public UpsMoney? ServiceOptionsCharges { get; set; }

        [JsonProperty("ItemizedCharges")]
        public UpsItemizedCharge[]? ItemizedCharges { get; set; }

        [JsonProperty("TotalCharges")]
        public UpsMoney? TotalCharges { get; set; }

        [JsonProperty("NegotiatedRateCharges")]
        public UpsNegotiatedRateCharges? NegotiatedRateCharges { get; set; }

        [JsonProperty("GuaranteedDelivery")]
        public UpsGuaranteedDelivery? GuaranteedDelivery { get; set; }

        [JsonProperty("TimeInTransit")]
        public UpsTimeInTransit? TimeInTransit { get; set; }
    }

    public class UpsMoney
    {
        [JsonProperty("CurrencyCode")]
        public string? CurrencyCode { get; set; }

        [JsonProperty("MonetaryValue")]
        public string? MonetaryValue { get; set; }
    }

    public class UpsBillingWeight
    {
        [JsonProperty("UnitOfMeasurement")]
        public UpsCodeDescription? UnitOfMeasurement { get; set; }

        [JsonProperty("Weight")]
        public string? Weight { get; set; }
    }

    public class UpsItemizedCharge
    {
        [JsonProperty("Code")]
        public string? Code { get; set; }

        [JsonProperty("Description")]
        public string? Description { get; set; }

        [JsonProperty("CurrencyCode")]
        public string? CurrencyCode { get; set; }

        [JsonProperty("MonetaryValue")]
        public string? MonetaryValue { get; set; }

        [JsonProperty("SubType")]
        public string? SubType { get; set; }
    }

    public class UpsNegotiatedRateCharges
    {
        [JsonProperty("TotalCharge")]
        public UpsMoney? TotalCharge { get; set; }

        [JsonProperty("TotalChargesWithTaxes")]
        public UpsMoney? TotalChargesWithTaxes { get; set; }

        [JsonProperty("ItemizedCharges")]
        public UpsItemizedCharge[]? ItemizedCharges { get; set; }
    }

    public class UpsGuaranteedDelivery
    {
        [JsonProperty("BusinessDaysInTransit")]
        public string? BusinessDaysInTransit { get; set; }

        [JsonProperty("DeliveryByTime")]
        public string? DeliveryByTime { get; set; }

        [JsonProperty("ScheduledDeliveryDate")]
        public string? ScheduledDeliveryDate { get; set; }
    }

    public class UpsTimeInTransit
    {
        [JsonProperty("ServiceSummary")]
        public UpsServiceSummary? ServiceSummary { get; set; }
    }

    public class UpsServiceSummary
    {
        [JsonProperty("EstimatedArrival")]
        public UpsEstimatedArrival? EstimatedArrival { get; set; }
    }

    public class UpsEstimatedArrival
    {
        [JsonProperty("BusinessDaysInTransit")]
        public string? BusinessDaysInTransit { get; set; }
    }
}
