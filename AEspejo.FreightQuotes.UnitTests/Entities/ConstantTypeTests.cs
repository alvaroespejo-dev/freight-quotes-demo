using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.UnitTests.Entities;

public class ConstantTypeTests
{
    [Fact]
    public void ConstantType_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var id = 3L;
        var name = "WeightType";
        var code = "WT";

        // Act
        var constantType = new ConstantType
        {
            Id = id,
            Name = name,
            Code = code
        };

        // Assert
        Assert.Equal(id, constantType.Id);
        Assert.Equal(name, constantType.Name);
        Assert.Equal(code, constantType.Code);
    }
}