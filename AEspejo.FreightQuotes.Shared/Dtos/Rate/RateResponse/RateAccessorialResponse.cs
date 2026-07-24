using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse
{
    public class RateAccessorialResponse
    {
        public long? AccessorialId { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
