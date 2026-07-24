namespace AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;

public class RateQuoteRequest
{
    public string RequestId { get; set; } = default!;
    public DateTime ShipDate { get; set; }
    public long? TermsId { get; set; }
    public long? RoleId { get; set; }
    public RateQuoteAddress BillingAddress { get; set; } = default!;
    public RateQuoteAddress OriginAddress { get; set; } = default!;
    public RateQuoteAddress DestinationAddress { get; set; } = default!;
    public IEnumerable<RateAccessorialRequest> Accessorials { get; set; } = [];
    public List<RateQuoteLineItem> LineItems { get; set; } = [];
    public long? EquipmentTypeId { get; set; }
    public int? TotalShippingUnits { get; set; }
    public decimal? Mileage { get; set; }
    public bool IncludeLtlQuotes { get; set; }
    public bool IncludeVolumeQuotes { get; set; }
    public bool IncludeGuaranteedQuotes { get; set; }
    public bool IncludeMabdQuotes { get; set; }
}