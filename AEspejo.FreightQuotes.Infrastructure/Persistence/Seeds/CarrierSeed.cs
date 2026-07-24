using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Seeds;

public class CarrierSeed : IEntityTypeConfiguration<Carrier>
{
    public void Configure(EntityTypeBuilder<Carrier> builder)
    {
        builder.HasData(
            new Carrier { Id = 1, Name = "Fedex", Scac = "FXFE", IsActive = true, IsMockMode = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Carrier { Id = 2, Name = "Estes", Scac = "EXLA", IsActive = true, IsMockMode = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Carrier { Id = 3, Name = "UPS", Scac = "UPS", IsActive = true, IsMockMode = true, CreatedUTC = SeedDefaults.CreatedUTC }
        );
    }
}
