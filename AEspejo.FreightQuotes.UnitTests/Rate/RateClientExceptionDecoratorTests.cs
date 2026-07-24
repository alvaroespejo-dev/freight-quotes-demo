using AEspejo.FreightQuotes.CarrierApiClient.Interfaces;
using AEspejo.FreightQuotes.CarrierApiClient.Rate;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Rate;

public class RateClientExceptionDecoratorTests
{
    private readonly Mock<ICarrierRateClient> _inner = new();
    private readonly Carrier _carrier = RateTestData.Carrier("UPS");

    private RateClientExceptionDecorator CreateSut() =>
        new(_inner.Object, NullLogger<RateClientExceptionDecorator>.Instance);

    private void SetupInner(Func<Task<IReadOnlyList<RateQuoteResponse>>> behavior) =>
        _inner.Setup(c => c.GetQuoteAsync(It.IsAny<RateQuoteRequest>(), It.IsAny<Carrier>(), It.IsAny<CancellationToken>()))
            .Returns(behavior);

    [Fact]
    public async Task GetQuoteAsync_PassesThroughInnerResult()
    {
        var expected = new List<RateQuoteResponse> { new() { CarrierName = "UPS" } };
        SetupInner(() => Task.FromResult<IReadOnlyList<RateQuoteResponse>>(expected));

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), _carrier, CancellationToken.None);

        Assert.Same(expected, result);
    }

    [Fact]
    public async Task GetQuoteAsync_InnerThrows_ReturnsErrorQuote()
    {
        SetupInner(() => throw new InvalidOperationException("boom"));

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), _carrier, CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.True(quote.HasError);
        Assert.Contains("boom", quote.Note);
    }

    [Fact]
    public async Task GetQuoteAsync_PerCallTimeout_ReturnsTimeoutQuote()
    {
        // ct is NOT cancelled -> the OperationCanceledException came from the client's timeout scope.
        SetupInner(() => throw new OperationCanceledException());

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), _carrier, CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.True(quote.HasError);
        Assert.Contains("timed out", quote.Note);
    }

    [Fact]
    public async Task GetQuoteAsync_CallerCancellation_Rethrows()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        SetupInner(() => throw new OperationCanceledException(cts.Token));

        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), _carrier, cts.Token));
    }
}
