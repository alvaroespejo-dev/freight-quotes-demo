using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Seeds;

public class CarrierSettingSeed : IEntityTypeConfiguration<CarrierSetting>
{
    // SettingType Constant ids:        Rating = 99, Authentication = 100
    // CarrierSettingType Constant ids: URL = 101, ClientId = 102, ClientSecret = 103,
    //                                  UserName = 104, Password = 105, ApiKey = 106,
    //                                  Account = 107, AccountSecundary = 108
    // Values are intentionally left empty; real endpoints/credentials are configured at runtime.
    public void Configure(EntityTypeBuilder<CarrierSetting> builder)
    {
        builder.HasData(
            // Fedex (CarrierId = 1)
            new CarrierSetting { Id = 1, CarrierId = 1, SettingTypeId = 99, CarrierSettingTypeId = 101, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },  // Rating / URL
            new CarrierSetting { Id = 2, CarrierId = 1, SettingTypeId = 99, CarrierSettingTypeId = 107, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },  // Rating / Account
            new CarrierSetting { Id = 3, CarrierId = 1, SettingTypeId = 99, CarrierSettingTypeId = 108, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },  // Rating / AccountSecundary
            new CarrierSetting { Id = 4, CarrierId = 1, SettingTypeId = 100, CarrierSettingTypeId = 101, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC }, // Authentication / URL
            new CarrierSetting { Id = 5, CarrierId = 1, SettingTypeId = 100, CarrierSettingTypeId = 102, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC }, // Authentication / ClientId
            new CarrierSetting { Id = 6, CarrierId = 1, SettingTypeId = 100, CarrierSettingTypeId = 103, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC }, // Authentication / ClientSecret

            // Estes (CarrierId = 2)
            new CarrierSetting { Id = 7, CarrierId = 2, SettingTypeId = 99, CarrierSettingTypeId = 101, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },   // Rating / URL
            new CarrierSetting { Id = 8, CarrierId = 2, SettingTypeId = 99, CarrierSettingTypeId = 106, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },   // Rating / ApiKey
            new CarrierSetting { Id = 9, CarrierId = 2, SettingTypeId = 100, CarrierSettingTypeId = 101, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },  // Authentication / URL
            new CarrierSetting { Id = 10, CarrierId = 2, SettingTypeId = 100, CarrierSettingTypeId = 106, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC }, // Authentication / ApiKey
            new CarrierSetting { Id = 11, CarrierId = 2, SettingTypeId = 100, CarrierSettingTypeId = 104, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC }, // Authentication / UserName
            new CarrierSetting { Id = 12, CarrierId = 2, SettingTypeId = 100, CarrierSettingTypeId = 105, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC }  // Authentication / Password
        );
    }
}
