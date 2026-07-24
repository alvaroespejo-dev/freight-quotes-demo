using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Repository;

public class ConstantRepositoryTests
{
    [Fact]
    public async Task GetByConstantTypeIdsAsync_ReturnsConstants()
    {
        // Arrange
        var typeIds = new List<long> { 1, 2 };
        var expectedConstants = new List<Constant>
        {
            new() { Id = 1, Name = "MaxWeight", Code = "1000", Order = 1 },
            new() { Id = 2, Name = "MinWeight", Code = "10", Order = 2 }
        };

        var mockRepo = new Mock<IConstantRepository>();
        mockRepo.Setup(r => r.GetByConstantTypeIdsAsync(typeIds, default))
                .ReturnsAsync(expectedConstants);

        // Act
        var result = await mockRepo.Object.GetByConstantTypeIdsAsync(typeIds, default);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Name == "MaxWeight");
        Assert.Contains(result, c => c.Name == "MinWeight");
    }
}