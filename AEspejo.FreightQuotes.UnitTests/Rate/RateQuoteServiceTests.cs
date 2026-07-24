using AEspejo.FreightQuotes.Application.Services;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Constants;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Rate;

/// <summary>
/// Verifies the keyed-DI resolution in <see cref="RateQuoteService"/>: mock mode uses the MOCK key,
/// a real carrier uses its (upper-cased) SCAC, and an unregistered SCAC throws.
/// </summary>
public class RateQuoteServiceTests
{
    private readonly Mock<ICarrierRateClient> _client = new();
    private readonly Mock<IServiceProvider> _provider = new();

    private RateQuoteService CreateSut() => new(_provider.Object);

    private void RegisterKeyed(object key, ICarrierRateClient? client) =>
        _provider.As<IKeyedServiceProvider>()
            .Setup(p => p.GetKeyedService(typeof(ICarrierRateClient), key))
            .Returns(client);

    private IReadOnlyList<RateQuoteResponse> Expected { get; } = new List<RateQuoteResponse> { new() { CarrierName = "X" } };

    public RateQuoteServiceTests()
    {
        _client.Setup(c => c.GetQuoteAsync(It.IsAny<RateQuoteRequest>(), It.IsAny<Carrier>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Expected);
    }

    [Fact]
    public async Task GetQuotesAsync_MockCarrier_ResolvesMockKey()
    {
        RegisterKeyed(CarrierScacConstant.MOCK, _client.Object);

        var result = await CreateSut().GetQuotesAsync(RateTestData.ValidRequest(), RateTestData.Carrier("UPS", mock: true));

        Assert.Same(Expected, result);
    }

    [Fact]
    public async Task GetQuotesAsync_RealCarrier_ResolvesByScac()
    {
        RegisterKeyed(CarrierScacConstant.UPS, _client.Object);

        var result = await CreateSut().GetQuotesAsync(RateTestData.ValidRequest(), RateTestData.Carrier("UPS"));

        Assert.Same(Expected, result);
    }

    [Fact]
    public async Task GetQuotesAsync_LowercaseScac_ResolvesUpperCasedKey()
    {
        RegisterKeyed(CarrierScacConstant.UPS, _client.Object);

        var result = await CreateSut().GetQuotesAsync(RateTestData.ValidRequest(), RateTestData.Carrier("ups"));

        Assert.Same(Expected, result);
    }

    [Fact]
    public async Task GetQuotesAsync_UnknownScac_Throws()
    {
        // No keyed client registered for this SCAC -> GetKeyedService returns null.
        _provider.As<IKeyedServiceProvider>()
            .Setup(p => p.GetKeyedService(It.IsAny<Type>(), It.IsAny<object>()))
            .Returns((ICarrierRateClient?)null);

        await Assert.ThrowsAsync<NotSupportedException>(() =>
            CreateSut().GetQuotesAsync(RateTestData.ValidRequest(), RateTestData.Carrier("XXX")));
    }
}
