using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.UnitTests.Entities;

public class StateTests
{
    [Fact]
    public void State_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var countryId = 1L;
        var name = "California";
        var code = "CA";
        var country = new Country { Id = countryId, Name = "USA", Code = "US" };

        // Act
        var state = new State
        {
            CountryId = countryId,
            Country = country,
            Name = name,
            Code = code
        };

        // Assert
        Assert.Equal(countryId, state.CountryId);
        Assert.Equal(country, state.Country);
        Assert.Equal(name, state.Name);
        Assert.Equal(code, state.Code);
    }
}
