using System.ComponentModel.DataAnnotations;

namespace AEspejo.FreightQuotes.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public DateTime CreatedUTC { get; set; }
    public DateTime? LastModifiedUTC { get; set; }
}