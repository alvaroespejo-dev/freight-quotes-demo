using System.ComponentModel.DataAnnotations;

namespace AEspejo.FreightQuotes.Domain.Entities;

public class ConstantType : BaseEntity
{
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public ICollection<Constant> Constants { get; set; } = [];
}
