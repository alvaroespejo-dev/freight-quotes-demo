using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Seeds;

public class ConstantTypeSeed : IEntityTypeConfiguration<ConstantType>
{
    public void Configure(EntityTypeBuilder<ConstantType> builder)
    {
        builder.HasData(
            new ConstantType { Id = 1, Name = "ShippingUnits", Code = "ShippingUnits", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new ConstantType { Id = 2, Name = "SubClass", Code = "SubClass", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new ConstantType { Id = 3, Name = "FreightClass", Code = "FreightClass", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new ConstantType { Id = 4, Name = "Accessorials", Code = "Accessorials", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new ConstantType { Id = 5, Name = "PartyAddressType", Code = "PartyAddressType", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new ConstantType { Id = 6, Name = "EquipmentType", Code = "EquipmentType", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new ConstantType { Id = 7, Name = "SettingType", Code = "SettingType", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new ConstantType { Id = 8, Name = "CarrierSettingType", Code = "CarrierSettingType", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new ConstantType { Id = 9, Name = "Terms", Code = "Terms", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new ConstantType { Id = 10, Name = "Role", Code = "Role", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC }
        );
    }
}

