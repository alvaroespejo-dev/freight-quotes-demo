using System.ComponentModel.DataAnnotations;

namespace AEspejo.FreightQuotes.Domain.Entities;

public class Accessorial : BaseEntity
{
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;
    public long TypeId { get; set; } 
    public Constant Type { get; set; } = null!;
    public bool IsActive { get; set; } = true;    
}