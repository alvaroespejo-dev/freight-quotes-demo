using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.UnitTests.Entities;

public class AccessorialTests
{
    [Fact]
    public void Accessorial_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var id = 1L;
        var name = "Liftgate";
        var code = "LG";
        var typeId = 2L;

        // Act
        var accessorial = new Accessorial
        {
            Id = id,
            Name = name,
            Code = code,
            TypeId = typeId
        };

        // Assert
        Assert.Equal(id, accessorial.Id);
        Assert.Equal(name, accessorial.Name);
        Assert.Equal(code, accessorial.Code);
        Assert.Equal(typeId, accessorial.TypeId);
    }
}