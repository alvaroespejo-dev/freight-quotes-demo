using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEspejo.FreightQuotes.Domain.Entities;

public class Constant : BaseEntity
{
    public long ConstantTypeId { get; set; }
    public ConstantType ConstantType { get; set; } = null!;
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsActive { get; set; } = true;
}
