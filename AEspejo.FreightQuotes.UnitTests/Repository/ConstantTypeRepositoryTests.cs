using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Repository;

public class ConstantTypeRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllConstantTypes()
    {
        // Arrange
        var expectedTypes = new List<ConstantType>
        {
            new ConstantType { Id = 1, Name = "WeightType", Code = "WT" },
            new ConstantType { Id = 2, Name = "SizeType", Code = "ST" }
        };

        var mockRepo = new Mock<IConstantTypeRepository>();
        mockRepo.Setup(r => r.GetAllAsync(default))
                .ReturnsAsync(expectedTypes);

        // Act
        var result = await mockRepo.Object.GetAllAsync(default);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, t => t.Name == "WeightType");
        Assert.Contains(result, t => t.Name == "SizeType");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectConstantType()
    {
        // Arrange
        var typeId = 1L;
        var expectedType = new ConstantType { Id = typeId, Name = "WeightType", Code = "WT" };

        var mockRepo = new Mock<IConstantTypeRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(typeId, default))
                .ReturnsAsync(expectedType);

        // Act
        var result = await mockRepo.Object.GetByIdAsync(typeId, default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(typeId, result.Id);
        Assert.Equal("WeightType", result.Name);
    }
}
