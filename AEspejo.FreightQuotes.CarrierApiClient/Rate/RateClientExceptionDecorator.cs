using AEspejo.FreightQuotes.CarrierApiClient.ApiCall;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;
using Microsoft.Extensions.Logging;

namespace AEspejo.FreightQuotes.CarrierApiClient.Rate
{
    /// <summary>
    /// Cross-cutting exception handling for any <see cref="ICarrierRateClient"/>. Wraps the real client so
    /// each implementation only expresses its happy path: unexpected exceptions are logged and turned into an
    /// error <see cref="RateQuoteResponse"/>, per-call timeouts into a timeout quote, and a genuine caller
    /// cancellation is re-thrown so the orchestrator can stop the whole request.
    /// </summary>
    public class RateClientExceptionDecorator(ICarrierRateClient inner, ILogger<RateClientExceptionDecorator> log)
        : ICarrierRateClient
    {
        private readonly ICarrierRateClient _inner = inner;
        private readonly ILogger<RateClientExceptionDecorator> _log = log;

        public async Task<IReadOnlyList<RateQuoteResponse>> GetQuoteAsync(
            RateQuoteRequest freightQuote, Carrier carrier, CancellationToken ct)
        {
            try
            {
                return await _inner.GetQuoteAsync(freightQuote, carrier, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                // A genuine caller cancellation must bubble up so the orchestrator can stop the whole request.
                throw;
            }
            catch (OperationCanceledException exception)
            {
                // Otherwise the cancellation came from the client's per-call timeout scope.
                _log.LogError(exception, "Carrier rate API timeout ({Carrier})", carrier.Name);
                return [RateQuoteError.Timeout(carrier)];
            }
            catch (Exception exception)
            {
                _log.LogError(exception, "Carrier rate API exception ({Carrier})", carrier.Name);
                return [RateQuoteError.From(carrier, exception)];
            }
        }
    }
}
