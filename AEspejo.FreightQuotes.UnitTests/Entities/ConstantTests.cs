using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.UnitTests.Entities;

public class ConstantTests
{
    [Fact]
    public void Constant_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var id = 5L;
        var name = "MaxWeight";
        var code = "1000";
        var order = 1000;

        // Act
        var constant = new Constant
        {
            Id = id,
            Name = name,
            Code = code,
            Order = order
        };

        // Assert
        Assert.Equal(id, constant.Id);
        Assert.Equal(name, constant.Name);
        Assert.Equal(code, constant.Code);
        Assert.Equal(order, constant.Order);
    }
}