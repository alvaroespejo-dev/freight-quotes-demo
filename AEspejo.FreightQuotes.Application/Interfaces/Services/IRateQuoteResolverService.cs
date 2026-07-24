using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;

namespace AEspejo.FreightQuotes.Application.Interfaces.Services
{
    /// <summary>
    /// Enriches a <see cref="RateQuoteRequest"/> with the resolved codes that real carrier APIs need
    /// (state/country codes, freight-class and shipping-unit codes), derived from the id-only payload.
    /// </summary>
    public interface IRateQuoteResolverService
    {
        Task ResolveAsync(RateQuoteRequest request, CancellationToken ct);
    }
}
