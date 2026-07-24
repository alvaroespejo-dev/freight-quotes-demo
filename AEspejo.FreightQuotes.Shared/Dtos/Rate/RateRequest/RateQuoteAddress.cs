using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;

public class RateQuoteAddress
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public long StateId { get; set; }
    public long CountryId { get; set; }
    /// <summary>
    /// Resolved state/province code (e.g. "CA"). Populated by the Application layer from <see cref="StateId"/>
    /// when building a request for a real carrier API. Optional for mock quoting.
    /// </summary>
    public string? StateCode { get; set; }
    /// <summary>
    /// Resolved ISO alpha-2 country code (e.g. "US"). Populated by the Application layer from <see cref="CountryId"/>
    /// when building a request for a real carrier API. Optional for mock quoting.
    /// </summary>
    public string? CountryCode { get; set; }
    public string Zip { get; set; } = string.Empty;
    public long? DockTypeId { get; set; }
    public bool AppointmentRequired { get; set; }
    public string? Notes { get; set; }
    public IEnumerable<RateAccessorialRequest> Accessorials { get; set; } = [];
}