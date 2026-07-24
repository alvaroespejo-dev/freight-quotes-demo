using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Repository;

public class StateRepositoryTests
{
    [Fact]
    public async Task GetByCountryIdAsync_ReturnsStatesForCountry()
    {
        // Arrange
        var countryId = 1L;
        var expectedStates = new List<State>
        {
            new() { Id = 1, CountryId = countryId, Name = "California", Code = "CA" },
            new() { Id = 2, CountryId = countryId, Name = "Texas", Code = "TX" }
        };

        var mockRepo = new Mock<IStateRepository>();
        mockRepo.Setup(r => r.GetByCountryIdAsync(countryId, default))
                .ReturnsAsync(expectedStates);

        // Act
        var result = await mockRepo.Object.GetByCountryIdAsync(countryId, default);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, s => Assert.Equal(countryId, s.CountryId));
    }
}
