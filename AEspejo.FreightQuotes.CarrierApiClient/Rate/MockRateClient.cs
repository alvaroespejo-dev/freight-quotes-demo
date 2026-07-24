using AEspejo.FreightQuotes.CarrierApiClient.Interfaces;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateResponse;

namespace AEspejo.FreightQuotes.CarrierApiClient.Rate
{
    public class MockRateClient : ICarrierRateClient
    {
        private static readonly Random _random = new();
        private const int MinDelayMs = 400;
        private const int MaxDelayMs = 2200;
        private const int MinCharge = 80;
        private const int MaxCharge = 600;
        private const int MinTransitDays = 1;
        private const int MaxTransitDays = 8;
        private const decimal BaseChargeRatio = 0.7M;
        private const decimal AccessorialChargeRatio = 0.3M;

        public async Task<IReadOnlyList<RateQuoteResponse>> GetQuoteAsync(
        RateQuoteRequest freightQuote,
        Carrier carrier,
        CancellationToken ct)
        {
            // Simulate carrier API call with random delay and data
            await SimulateNetworkDelay(ct);
            var (charge, days) = GenerateRandomQuoteData();
            return [CreateQuoteResponse(carrier, charge, days)];
        }

        private async Task SimulateNetworkDelay(CancellationToken ct)
        {
            var delay = _random.Next(MinDelayMs, MaxDelayMs);
            await Task.Delay(delay, ct);
        }

        private (int charge, int days) GenerateRandomQuoteData()
        {
            var charge = _random.Next(MinCharge, MaxCharge);
            var days = _random.Next(MinTransitDays, MaxTransitDays);
            return (charge, days);
        }

        private RateQuoteResponse CreateQuoteResponse(Carrier carrier, int charge, int days)
        {
            return new RateQuoteResponse
            {
                CarrierId = carrier.Id,
                CarrierName = carrier.Name,
                ServiceLevel = "STD",
                BaseCharge = charge * BaseChargeRatio,
                AccessorialCharge = charge * AccessorialChargeRatio,
                TotalCharge = charge,
                TransitDays = days,
                Note = $"{carrier.Name} standard service",
            };
        }
    }
}
