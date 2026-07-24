using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Constant = AEspejo.FreightQuotes.Domain.Entities.Constant;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Seeds;

public class ConstantSeed : IEntityTypeConfiguration<Constant>
{
    public void Configure(EntityTypeBuilder<Constant> builder)
    {
        builder.HasData(

            // ShippingUnits (ConstantTypeId = 1)
            new Constant { Id = 1, ConstantTypeId = 1, Name = "Bags", Code = "Bags", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 2, ConstantTypeId = 1, Name = "Bales", Code = "Bales", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 3, ConstantTypeId = 1, Name = "Barrels", Code = "Barrels", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 4, ConstantTypeId = 1, Name = "Base Plate", Code = "BasePlate", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 5, ConstantTypeId = 1, Name = "Baskets", Code = "Baskets", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 6, ConstantTypeId = 1, Name = "Boxes", Code = "Boxes", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 7, ConstantTypeId = 1, Name = "Buckets", Code = "Buckets", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 8, ConstantTypeId = 1, Name = "Bulkheads", Code = "Bulkheads", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 9, ConstantTypeId = 1, Name = "Bundles", Code = "Bundles", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 10, ConstantTypeId = 1, Name = "Carboys", Code = "Carboys", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 11, ConstantTypeId = 1, Name = "Carrier", Code = "Carrier", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 12, ConstantTypeId = 1, Name = "Cartons", Code = "Cartons", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 13, ConstantTypeId = 1, Name = "Carts", Code = "Carts", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 14, ConstantTypeId = 1, Name = "Cases", Code = "Cases", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 15, ConstantTypeId = 1, Name = "Coils", Code = "Coils", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 16, ConstantTypeId = 1, Name = "Crate", Code = "Crate", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 17, ConstantTypeId = 1, Name = "Drums", Code = "Drums", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 18, ConstantTypeId = 1, Name = "Eaches", Code = "Eaches", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 19, ConstantTypeId = 1, Name = "Empty Containers", Code = "EmptyContainers", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 20, ConstantTypeId = 1, Name = "Empty Drums", Code = "EmptyDrums", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 21, ConstantTypeId = 1, Name = "Empty Totes", Code = "EmptyTotes", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 22, ConstantTypeId = 1, Name = "Feet", Code = "Feet", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 23, ConstantTypeId = 1, Name = "Firkins", Code = "Firkins", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 24, ConstantTypeId = 1, Name = "Gaylords", Code = "Gaylords", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 25, ConstantTypeId = 1, Name = "Hampers", Code = "Hampers", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 26, ConstantTypeId = 1, Name = "Hogsheads", Code = "Hogsheads", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 27, ConstantTypeId = 1, Name = "Kegs", Code = "Kegs", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 28, ConstantTypeId = 1, Name = "Models", Code = "Models", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 29, ConstantTypeId = 1, Name = "Packages", Code = "Packages", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 30, ConstantTypeId = 1, Name = "Pails", Code = "Pails", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 31, ConstantTypeId = 1, Name = "Pallets", Code = "Pallets", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 32, ConstantTypeId = 1, Name = "Pieces", Code = "Pieces", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 33, ConstantTypeId = 1, Name = "Racks", Code = "Racks", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 34, ConstantTypeId = 1, Name = "Reels", Code = "Reels", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 35, ConstantTypeId = 1, Name = "Rolls", Code = "Rolls", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 36, ConstantTypeId = 1, Name = "Skid", Code = "Skid", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 37, ConstantTypeId = 1, Name = "Slip Sheets", Code = "SlipSheets", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 38, ConstantTypeId = 1, Name = "Sows", Code = "Sows", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 39, ConstantTypeId = 1, Name = "Super Sack", Code = "SuperSack", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 40, ConstantTypeId = 1, Name = "Tanks", Code = "Tanks", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 41, ConstantTypeId = 1, Name = "Totes", Code = "Totes", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 42, ConstantTypeId = 1, Name = "Trunks", Code = "Trunks", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 43, ConstantTypeId = 1, Name = "Tubes", Code = "Tubes", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 44, ConstantTypeId = 1, Name = "Unpackaged", Code = "Unpackaged", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // SubClass (ConstantTypeId = 2)
            new Constant { Id = 45, ConstantTypeId = 2, Name = "1", Code = "1", Order = 1, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 46, ConstantTypeId = 2, Name = "2", Code = "2", Order = 2, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 47, ConstantTypeId = 2, Name = "3", Code = "3", Order = 3, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 48, ConstantTypeId = 2, Name = "4", Code = "4", Order = 4, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 49, ConstantTypeId = 2, Name = "5", Code = "5", Order = 5, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 50, ConstantTypeId = 2, Name = "6", Code = "6", Order = 6, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 51, ConstantTypeId = 2, Name = "7", Code = "7", Order = 7, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 52, ConstantTypeId = 2, Name = "8", Code = "8", Order = 8, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 53, ConstantTypeId = 2, Name = "9", Code = "9", Order = 9, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 54, ConstantTypeId = 2, Name = "10", Code = "10", Order = 10, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 55, ConstantTypeId = 2, Name = "11", Code = "11", Order = 11, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 56, ConstantTypeId = 2, Name = "12", Code = "12", Order = 12, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 57, ConstantTypeId = 2, Name = "13", Code = "13", Order = 13, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 58, ConstantTypeId = 2, Name = "14", Code = "14", Order = 14, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 59, ConstantTypeId = 2, Name = "15", Code = "15", Order = 15, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 60, ConstantTypeId = 2, Name = "16", Code = "16", Order = 16, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 61, ConstantTypeId = 2, Name = "17", Code = "17", Order = 17, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 62, ConstantTypeId = 2, Name = "18", Code = "18", Order = 18, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 63, ConstantTypeId = 2, Name = "19", Code = "19", Order = 19, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 64, ConstantTypeId = 2, Name = "20", Code = "20", Order = 20, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // FreightClass (ConstantTypeId = 3)
            new Constant { Id = 65, ConstantTypeId = 3, Name = "50", Code = "50", Order = 1, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 66, ConstantTypeId = 3, Name = "55", Code = "55", Order = 2, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 67, ConstantTypeId = 3, Name = "60", Code = "60", Order = 3, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 68, ConstantTypeId = 3, Name = "65", Code = "65", Order = 4, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 69, ConstantTypeId = 3, Name = "70", Code = "70", Order = 5, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 70, ConstantTypeId = 3, Name = "77.5", Code = "77.5", Order = 6, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 71, ConstantTypeId = 3, Name = "85", Code = "85", Order = 7, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 72, ConstantTypeId = 3, Name = "92.5", Code = "92.5", Order = 8, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 73, ConstantTypeId = 3, Name = "100", Code = "100", Order = 9, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 74, ConstantTypeId = 3, Name = "110", Code = "110", Order = 10, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 75, ConstantTypeId = 3, Name = "125", Code = "125", Order = 11, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 76, ConstantTypeId = 3, Name = "150", Code = "150", Order = 12, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 77, ConstantTypeId = 3, Name = "175", Code = "175", Order = 13, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 78, ConstantTypeId = 3, Name = "200", Code = "200", Order = 14, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 79, ConstantTypeId = 3, Name = "250", Code = "250", Order = 15, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 80, ConstantTypeId = 3, Name = "300", Code = "300", Order = 16, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 81, ConstantTypeId = 3, Name = "400", Code = "400", Order = 17, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 82, ConstantTypeId = 3, Name = "500", Code = "500", Order = 18, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // Accessorials (ConstantTypeId = 4)
            new Constant { Id = 83, ConstantTypeId = 4, Name = "General", Code = "General", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 84, ConstantTypeId = 4, Name = "Dock Type", Code = "DockType", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 85, ConstantTypeId = 4, Name = "Origin and Destination", Code = "OD", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 86, ConstantTypeId = 4, Name = "Item", Code = "Item", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 87, ConstantTypeId = 4, Name = "Other", Code = "Other", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // PartyAddressType (ConstantTypeId = 5)
            new Constant { Id = 88, ConstantTypeId = 5, Name = "Billing", Code = "B", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 89, ConstantTypeId = 5, Name = "Origin", Code = "O", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 90, ConstantTypeId = 5, Name = "Destination", Code = "D", IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // EquipmentType (ConstantTypeId = 6)
            new Constant { Id = 91, ConstantTypeId = 6, Name = "Dry Van", Code = "DryVan", Order = 1, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 92, ConstantTypeId = 6, Name = "Reefer", Code = "Reefer", Order = 2, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 93, ConstantTypeId = 6, Name = "Flatbed", Code = "Flatbed", Order = 3, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 94, ConstantTypeId = 6, Name = "Step Deck", Code = "StepDeck", Order = 4, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 95, ConstantTypeId = 6, Name = "Straight Truck", Code = "StraightTruck", Order = 5, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 96, ConstantTypeId = 6, Name = "Sprinter Van", Code = "SprinterVan", Order = 6, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 97, ConstantTypeId = 6, Name = "Box Truck", Code = "BoxTruck", Order = 7, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 98, ConstantTypeId = 6, Name = "Power Only", Code = "PowerOnly", Order = 8, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // SettingType (ConstantTypeId = 7)
            new Constant { Id = 99, ConstantTypeId = 7, Name = "Rating", Code = "Rating", Order = 1, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 100, ConstantTypeId = 7, Name = "Authentication", Code = "Authentication", Order = 2, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            // CarrierSettingType (ConstantTypeId = 8)
            new Constant { Id = 101, ConstantTypeId = 8, Name = "URL", Code = "URL", Order = 1, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 102, ConstantTypeId = 8, Name = "ClientId", Code = "ClientId", Order = 2, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 103, ConstantTypeId = 8, Name = "ClientSecret", Code = "ClientSecret", Order = 3, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 104, ConstantTypeId = 8, Name = "UserName", Code = "UserName", Order = 4, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 105, ConstantTypeId = 8, Name = "Password", Code = "Password", Order = 5, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 106, ConstantTypeId = 8, Name = "ApiKey", Code = "ApiKey", Order = 6, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 107, ConstantTypeId = 8, Name = "Account", Code = "Account", Order = 7, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 108, ConstantTypeId = 8, Name = "AccountSecundary", Code = "AccountSecundary", Order = 8, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            new Constant { Id = 109, ConstantTypeId = 9, Name = "Collect", Code = "Collect", Order = 1, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 110, ConstantTypeId = 9, Name = "Prepaid", Code = "Prepaid", Order = 2, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 111, ConstantTypeId = 9, Name = "Third Party", Code = "ThirdParty", Order = 3, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },

            new Constant { Id = 112, ConstantTypeId = 10, Name = "Consignee", Code = "Consignee", Order = 1, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 113, ConstantTypeId = 10, Name = "Shipper", Code = "Shipper", Order = 2, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC },
            new Constant { Id = 114, ConstantTypeId = 10, Name = "Third Party", Code = "ThirdParty", Order = 3, IsActive = true, CreatedUTC = SeedDefaults.CreatedUTC }

        );
    }
}