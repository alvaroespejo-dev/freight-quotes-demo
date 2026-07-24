using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;

namespace AEspejo.FreightQuotes.CarrierApiClient.ApiCall
{
    /// <summary>
    /// Builds the error/timeout <see cref="RateQuoteResponse"/> items (<c>HasError = true</c>) that a carrier
    /// rate flow surfaces instead of throwing. Shared by the rate clients (domain-level errors such as
    /// validation/auth/no-quotes) and by <c>RateClientExceptionDecorator</c> (unexpected exceptions/timeouts).
    /// </summary>
    public static class RateQuoteError
    {
        public static RateQuoteResponse From(Carrier carrier, string message)
            => From(carrier, [message]);

        public static RateQuoteResponse From(Carrier carrier, Exception exception)
        {
            var message = !string.IsNullOrWhiteSpace(exception.InnerException?.Message)
                ? exception.InnerException!.Message
                : exception.Message;

            return From(carrier, [message]);
        }

        public static RateQuoteResponse From(Carrier carrier, IEnumerable<string> messages)
        {
            var note = string.Join(" | ", messages.Where(m => !string.IsNullOrWhiteSpace(m)));

            return new RateQuoteResponse
            {
                CarrierId = carrier.Id,
                CarrierName = carrier.Name,
                HasError = true,
                Note = string.IsNullOrWhiteSpace(note) ? "The carrier returned no quotes." : note,
            };
        }

        public static RateQuoteResponse Timeout(Carrier carrier, int? apiTimeoutMs = null)
        {
            var message = apiTimeoutMs.HasValue
                ? $"The request timed out after {Convert.ToDecimal(apiTimeoutMs.Value) / 1000:0.##} seconds."
                : "The request timed out.";

            return From(carrier, message);
        }
    }
}
