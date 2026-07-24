using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEspejo.FreightQuotes.Shared.Dtos.State;

public record StateResponse(long Id, long CountryId, string Name, string Code);