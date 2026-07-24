using System.ComponentModel.DataAnnotations;

namespace AEspejo.FreightQuotes.Domain.Entities;

public class CarrierSetting : BaseEntity
{
    public long CarrierId { get; set; }
    public Carrier Carrier { get; set; } = null!;

    // FK -> Constant (ConstantType "SettingType": Rating / Authentication)
    public long SettingTypeId { get; set; }
    public Constant SettingType { get; set; } = null!;

    // FK -> Constant (ConstantType "CarrierSettingType": URL / ApiKey / Account / ...)
    public long CarrierSettingTypeId { get; set; }
    public Constant CarrierSettingType { get; set; } = null!;

    [MaxLength(1000)]
    public string Value { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
