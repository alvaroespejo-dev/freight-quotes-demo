using System.ComponentModel.DataAnnotations;

namespace AEspejo.FreightQuotes.Domain.Entities;

public class Country : BaseEntity
{
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;
    public ICollection<State> States { get; set; } = [];
}

