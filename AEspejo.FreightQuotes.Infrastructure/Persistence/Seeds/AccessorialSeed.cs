using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Seeds;

public class AccessorialSeed : IEntityTypeConfiguration<Accessorial>
{
    public void Configure(EntityTypeBuilder<Accessorial> builder)
    {
        builder.HasData(

            // General (Constant = 83)
            new Accessorial { Id = 1, Name = "BlindShipment", Code = "BLS", TypeId = 83, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 2, Name = "Insurance", Code = "INS", TypeId = 83, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 3, Name = "Protect From Freeze", Code = "PFF", TypeId = 83, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 4, Name = "Sort and Segregate", Code = "SAS", TypeId = 83, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 5, Name = "TradeShow", Code = "TRS", TypeId = 83, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 45, Name = "COD", Code = "COD", TypeId = 83, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 46, Name = "In Bond", Code = "INB", TypeId = 83, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // DockType (Constant = 84)
            new Accessorial { Id = 6, Name = "Airport", Code = "AIR", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 7, Name = "Business", Code = "BUS", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 8, Name = "Church", Code = "CHU", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 9, Name = "Construction Site", Code = "CNS", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 10, Name = "Farm", Code = "FAR", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 11, Name = "Government", Code = "GOV", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 12, Name = "Grocery Warehouse", Code = "GRW", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 13, Name = "Hotel", Code = "HOT", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 14, Name = "LimitedAccess", Code = "LAC", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 15, Name = "Military Site", Code = "MIL", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 16, Name = "Mine", Code = "MIN", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 17, Name = "Nuclear", Code = "NUC", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 18, Name = "Other Limited Access", Code = "OLA", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 19, Name = "Prison", Code = "PRI", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 20, Name = "Residential", Code = "RES", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 21, Name = "School", Code = "SCH", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 22, Name = "Storage Facility", Code = "STO", TypeId = 84, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // OD (Constant = 85)
            new Accessorial { Id = 23, Name = "Appointment", Code = "APP", TypeId = 85, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 24, Name = "Inside", Code = "INP", TypeId = 85, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 25, Name = "LiftGate", Code = "LFG", TypeId = 85, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 26, Name = "Notification", Code = "NTF", TypeId = 85, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // Item (Constant = 86)
            new Accessorial { Id = 27, Name = "HazMat", Code = "HAZ", TypeId = 86, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 28, Name = "OverLength", Code = "OVL", TypeId = 86, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // Other (Constant = 87)
            new Accessorial { Id = 29, Name = "HST", Code = "HST", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 30, Name = "PST", Code = "PST", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 31, Name = "X border fee", Code = "XBF", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 32, Name = "High cost lane", Code = "HCL", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 33, Name = "Detention Destination", Code = "DTD", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 34, Name = "Detention Origin", Code = "DTO", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 35, Name = "LayOver", Code = "LAY", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 36, Name = "Lumper Service", Code = "LUM", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 37, Name = "Other", Code = "OTH", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 38, Name = "Reconsignment", Code = "REC", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 39, Name = "Tailgating - Driver Assist", Code = "TDA", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 40, Name = "Temperature Control", Code = "TPC", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 41, Name = "TONU", Code = "TON", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 42, Name = "W&I Fee", Code = "WIF", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 43, Name = "Additional Stops", Code = "ADS", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Accessorial { Id = 44, Name = "Show Final Mile Options", Code = "FMO", TypeId = 87, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC }
        );
    }
}