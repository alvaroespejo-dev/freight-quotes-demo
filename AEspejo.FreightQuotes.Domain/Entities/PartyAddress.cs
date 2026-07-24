using AEspejo.FreightQuotes.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace AEspejo.FreightQuotes.Domain.Entities;

public class PartyAddress : BaseEntity
{
    public long TypeId { get; set; }
    public Constant Type { get; set; } = null!;
    [MaxLength(200)]
    public string? Name { get; set; }
    [MaxLength(200)]
    public string? Address1 { get; set; }
    [MaxLength(200)]
    public string? Address2 { get; set; }
    [MaxLength(50)]
    public string? City { get; set; }
    public long StateId { get; set; }
    public State State { get; set; } = null!;
    public long CountryId { get; set; }
    public Country Country { get; set; } = null!;
    [MaxLength(15)]
    public string Zip { get; set; } = string.Empty;
}
