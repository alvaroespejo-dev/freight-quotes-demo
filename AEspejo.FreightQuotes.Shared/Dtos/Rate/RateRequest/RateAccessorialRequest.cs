using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;

public class RateAccessorialRequest
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal? Cost { get; set; }
}