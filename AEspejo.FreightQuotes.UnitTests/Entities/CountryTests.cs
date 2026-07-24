using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.UnitTests.Entities;

public class CountryTests
{
    [Fact]
    public void Country_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var id = 10L;
        var name = "Argentina";
        var code = "AR";

        // Act
        var country = new Country
        {
            Id = id,
            Name = name,
            Code = code
        };

        // Assert
        Assert.Equal(id, country.Id);
        Assert.Equal(name, country.Name);
        Assert.Equal(code, country.Code);
    }
}