using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse
{
    public class RateQuoteResponse
    {
        public long CarrierId { get; set; }
        public string CarrierName { get; set; } = string.Empty;
        public string QuoteNumber { get; set; } = string.Empty;
        public string ServiceLevel { get; set; } = string.Empty;
        public decimal BaseCharge { get; set; }
        public decimal AccessorialCharge { get; set; }
        public decimal TotalCharge { get; set; }
        public int TransitDays { get; set; }
        public string Note { get; set; } = string.Empty;
        public bool HasError { get; set; }
        public IEnumerable<RateAccessorialResponse> Accessorial { get; set; } = [];
    }
}