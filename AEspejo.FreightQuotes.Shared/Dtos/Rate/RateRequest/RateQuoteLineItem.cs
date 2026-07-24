namespace AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest
{
    public class RateQuoteLineItem
    {
        public long Qty { get; set; }
        public long UnitId { get; set; }
        public decimal Weight { get; set; }
        public string Nmfc { get; set; } = string.Empty;
        public int? SubClassId { get; set; }
        public int ClassId { get; set; }
        /// <summary>
        /// Resolved freight-class value (e.g. "50", "77.5", "300"). Populated by the Application layer from
        /// <see cref="ClassId"/> when building a request for a real carrier API. Optional for mock quoting.
        /// </summary>
        public string? FreightClass { get; set; }
        /// <summary>
        /// Resolved shipping-unit code (e.g. "Pallets", "Boxes"). Populated by the Application layer from
        /// <see cref="UnitId"/> when building a request for a real carrier API. Optional for mock quoting.
        /// </summary>
        public string? ShippingUnitCode { get; set; }
        public bool IsHazMat { get; set; }
        public string Description { get; set; } = string.Empty;
        public int ShipQty { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public bool IsStackable { get; set; }
    }
}
