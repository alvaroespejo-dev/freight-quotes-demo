using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Seeds;

public class StateSeed : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasData(

        // USA States
        new State { Id = 1, Name = "Alabama", Code = "AL", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 2, Name = "Alaska", Code = "AK", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 3, Name = "Arizona", Code = "AZ", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 4, Name = "Arkansas", Code = "AR", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 5, Name = "California", Code = "CA", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 6, Name = "Colorado", Code = "CO", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 7, Name = "Connecticut", Code = "CT", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 8, Name = "Delaware", Code = "DE", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 9, Name = "Florida", Code = "FL", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 10, Name = "Georgia", Code = "GA", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 11, Name = "Hawaii", Code = "HI", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 12, Name = "Idaho", Code = "ID", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 13, Name = "Illinois", Code = "IL", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 14, Name = "Indiana", Code = "IN", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 15, Name = "Iowa", Code = "IA", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 16, Name = "Kansas", Code = "KS", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 17, Name = "Kentucky", Code = "KY", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 18, Name = "Louisiana", Code = "LA", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 19, Name = "Maine", Code = "ME", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 20, Name = "Maryland", Code = "MD", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 21, Name = "Massachusetts", Code = "MA", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 22, Name = "Michigan", Code = "MI", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 23, Name = "Minnesota", Code = "MN", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 24, Name = "Mississippi", Code = "MS", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 25, Name = "Missouri", Code = "MO", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 26, Name = "Montana", Code = "MT", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 27, Name = "Nebraska", Code = "NE", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 28, Name = "Nevada", Code = "NV", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 29, Name = "New Hampshire", Code = "NH", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 30, Name = "New Jersey", Code = "NJ", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 31, Name = "New Mexico", Code = "NM", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 32, Name = "New York", Code = "NY", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 33, Name = "North Carolina", Code = "NC", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 34, Name = "North Dakota", Code = "ND", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 35, Name = "Ohio", Code = "OH", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 36, Name = "Oklahoma", Code = "OK", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 37, Name = "Oregon", Code = "OR", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 38, Name = "Pennsylvania", Code = "PA", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 39, Name = "Puerto Rico", Code = "PR", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 40, Name = "Rhode Island", Code = "RI", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 41, Name = "South Carolina", Code = "SC", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 42, Name = "South Dakota", Code = "SD", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 43, Name = "Tennessee", Code = "TN", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 44, Name = "Texas", Code = "TX", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 45, Name = "Utah", Code = "UT", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 46, Name = "Vermont", Code = "VT", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 47, Name = "Virginia", Code = "VA", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 48, Name = "Washington", Code = "WA", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 49, Name = "Washington DC", Code = "DC", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 50, Name = "West Virginia", Code = "WV", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 51, Name = "Wisconsin", Code = "WI", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 52, Name = "Wyoming", Code = "WY", CountryId = 1, CreatedUTC = SeedDefaults.CreatedUTC },

        // Canada Provinces
        new State { Id = 53, Name = "Alberta", Code = "AB", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 54, Name = "Canada Cross Border", Code = "CC", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 55, Name = "Canada Intra", Code = "CN", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 56, Name = "Colombie Britannique", Code = "BC", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 57, Name = "Île du Prince Édouard", Code = "PE", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 58, Name = "Manitoba", Code = "MB", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 59, Name = "Nouveau Brunswick", Code = "NB", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 60, Name = "Nouvelle Écosse", Code = "NS", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 61, Name = "Nunavut", Code = "NU", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 62, Name = "Ontario", Code = "ON", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 63, Name = "Québec", Code = "QC", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 64, Name = "Saskatchewan", Code = "SK", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 65, Name = "Terre Neuve et Labrador", Code = "NL", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 66, Name = "Territoires du Nord Ouest", Code = "NT", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 67, Name = "Yukon", Code = "YT", CountryId = 2, CreatedUTC = SeedDefaults.CreatedUTC },

        // Mexico States
        new State { Id = 68, Name = "Aguascalientes", Code = "Ags", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 69, Name = "Baja California", Code = "BC", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 70, Name = "Baja California Sur", Code = "BCS", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 71, Name = "Campeche", Code = "Camp", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 72, Name = "Chiapas", Code = "CHIS", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 73, Name = "Chihuahua", Code = "CHIH", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 74, Name = "Coahuila", Code = "COAH", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 75, Name = "Colima", Code = "COL", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 76, Name = "Ciudad de México", Code = "CDMX", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 77, Name = "Durango", Code = "DGO", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 78, Name = "Guanajuato", Code = "GTO", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 79, Name = "Guerrero", Code = "GRO", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 80, Name = "Hidalgo", Code = "HGO", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 81, Name = "Jalisco", Code = "JAL", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 82, Name = "Estado de México", Code = "MEX", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 83, Name = "Michoacán", Code = "MICH", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 84, Name = "Morelos", Code = "MOR", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 85, Name = "Nayarit", Code = "NAY", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 86, Name = "Nuevo León", Code = "NL", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 87, Name = "Oaxaca", Code = "OAX", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 88, Name = "Puebla", Code = "PUE", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 89, Name = "Querétaro", Code = "QRO", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 90, Name = "Quintana Roo", Code = "QROO", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 91, Name = "San Luis Potosí", Code = "SLP", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 92, Name = "Sinaloa", Code = "SIN", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 93, Name = "Sonora", Code = "SON", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 94, Name = "Tabasco", Code = "TAB", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 95, Name = "Tamaulipas", Code = "Tamps", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 96, Name = "Tlaxcala", Code = "TLAX", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 97, Name = "Veracruz", Code = "VER", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 98, Name = "Yucatán", Code = "YUC", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },
        new State { Id = 99, Name = "Zacatecas", Code = "ZAC", CountryId = 3, CreatedUTC = SeedDefaults.CreatedUTC },

        // Other
        new State { Id = 100, Name = "Other", Code = "Other", CountryId = 4, CreatedUTC = SeedDefaults.CreatedUTC }   

        );
    }
}

