using System.ComponentModel.DataAnnotations;

namespace AEspejo.FreightQuotes.Domain.Entities;

public class State : BaseEntity
{
    public long CountryId { get; set; }
    public Country Country { get; set; } = null!;
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(8)]
    public string Code { get; set; } = string.Empty;
}
