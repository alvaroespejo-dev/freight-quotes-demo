using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;

namespace AEspejo.FreightQuotes.CarrierApiClient.ApiCall
{
    /// <summary>
    /// Base for carrier rate clients. Exposes helpers to build error/timeout <see cref="RateQuoteResponse"/>
    /// items (delegating to <see cref="RateQuoteError"/>) for the domain-level errors a client detects on its
    /// happy path (validation, auth, no quotes). Unexpected exceptions/timeouts are handled centrally by
    /// <c>RateClientExceptionDecorator</c>, so clients no longer wrap their logic in try/catch.
    /// </summary>
    public abstract class RateApiCallBase
    {
        protected static RateQuoteResponse ErrorQuote(Carrier carrier, string message)
            => RateQuoteError.From(carrier, message);

        protected static RateQuoteResponse ErrorQuote(Carrier carrier, Exception exception)
            => RateQuoteError.From(carrier, exception);

        protected static RateQuoteResponse ErrorQuote(Carrier carrier, IEnumerable<string> messages)
            => RateQuoteError.From(carrier, messages);

        protected static RateQuoteResponse TimeoutQuote(Carrier carrier, int? apiTimeoutMs)
            => RateQuoteError.Timeout(carrier, apiTimeoutMs);

        /// <summary>
        /// True when the accessorial list contains the given code (case-insensitive).
        /// </summary>
        protected static bool HasAccessorial(IEnumerable<RateAccessorialRequest> accessorials, string code)
            => accessorials.Any(a => string.Equals(a.Code, code, StringComparison.OrdinalIgnoreCase));
    }
}
