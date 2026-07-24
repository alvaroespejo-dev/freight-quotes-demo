using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Seeds;

public class CountrySeed : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasData(
            new Country { Id = 1, Name = "USA", Code = "USA", CreatedUTC = SeedDefaults.CreatedUTC },
            new Country { Id = 2, Name = "Canada", Code = "CAN", CreatedUTC = SeedDefaults.CreatedUTC },
            new Country { Id = 3, Name = "Mexico", Code = "MEX", CreatedUTC = SeedDefaults.CreatedUTC },
            new Country { Id = 4, Name = "Other", Code = "Other", CreatedUTC = SeedDefaults.CreatedUTC }
        );
    }
}
