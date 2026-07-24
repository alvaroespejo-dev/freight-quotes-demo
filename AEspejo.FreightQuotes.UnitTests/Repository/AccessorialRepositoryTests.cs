using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Repository;
public class AccessorialRepositoryTests
{
    [Fact]
    public async Task GetWithTypeByIdAsync_ReturnsAccessorialWithType()
    {
        // Arrange
        var accessorialId = 1L;
        var expectedAccessorial = new Accessorial
        {
            Id = accessorialId,
            Name = "Liftgate",
            Code = "LG",
            TypeId = 2,
            Type = new() { Id = 2, Name = "TypeA", Code = "TA" }
        };

        var mockRepo = new Mock<IAccessorialRepository>();
        mockRepo.Setup(r => r.GetWithTypeByIdAsync(accessorialId, default))
                .ReturnsAsync(expectedAccessorial);

        // Act
        var result = await mockRepo.Object.GetWithTypeByIdAsync(accessorialId, default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(accessorialId, result.Id);
        Assert.NotNull(result.Type);
        Assert.Equal("TypeA", result.Type.Name);
    }
}