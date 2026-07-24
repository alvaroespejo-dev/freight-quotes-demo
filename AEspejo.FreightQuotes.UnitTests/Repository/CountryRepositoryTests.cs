using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Repository;

public class CountryRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllCountries()
    {
        // Arrange
        var expectedCountries = new List<Country>
        {
            new() { Id = 1, Name = "USA", Code = "US" },
            new() { Id = 2, Name = "Canada", Code = "CA" }
        };

        var mockRepo = new Mock<ICountryRepository>();
        mockRepo.Setup(r => r.GetAllAsync(default))
                .ReturnsAsync(expectedCountries);

        // Act
        var result = await mockRepo.Object.GetAllAsync(default);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, c => c.Name == "USA");
        Assert.Contains(result, c => c.Name == "Canada");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectCountry()
    {
        // Arrange
        var countryId = 1L;
        var expectedCountry = new Country { Id = countryId, Name = "USA", Code = "US" };

        var mockRepo = new Mock<ICountryRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(countryId, default))
                .ReturnsAsync(expectedCountry);

        // Act
        var result = await mockRepo.Object.GetByIdAsync(countryId, default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(countryId, result.Id);
        Assert.Equal("USA", result.Name);
    }
}