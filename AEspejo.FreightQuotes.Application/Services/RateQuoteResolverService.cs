using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Application.Interfaces.Services;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Constants;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;

namespace AEspejo.FreightQuotes.Application.Services
{
    /// <summary>
    /// Resolves the id-only fields of a <see cref="RateQuoteRequest"/> into the string codes carrier APIs expect:
    /// address state/country codes, and per-line-item freight-class and shipping-unit codes.
    /// Runs once per rate request (carrier-independent) before the carriers are queried.
    /// </summary>
    public class RateQuoteResolverService(
        IStateRepository states,
        ICountryRepository countries,
        IConstantRepository constants) : IRateQuoteResolverService
    {
        private readonly IStateRepository _states = states;
        private readonly ICountryRepository _countries = countries;
        private readonly IConstantRepository _constants = constants;

        // Country.Code is stored as ISO alpha-3 ("USA"); FedEx (and most carrier APIs) expect ISO alpha-2 ("US").
        private static readonly Dictionary<string, string> _alpha3ToAlpha2 = new(StringComparer.OrdinalIgnoreCase)
        {
            ["USA"] = "US",
            ["CAN"] = "CA",
            ["MEX"] = "MX",
        };

        public async Task ResolveAsync(RateQuoteRequest request, CancellationToken ct)
        {
            var addresses = new[] { request.OriginAddress, request.DestinationAddress, request.BillingAddress }
                .Where(a => a is not null)
                .Cast<RateQuoteAddress>()
                .ToList();

            await ResolveAddressCodesAsync(addresses, ct);
            await ResolveLineItemCodesAsync(request.LineItems, ct);
        }

        private async Task ResolveAddressCodesAsync(List<RateQuoteAddress> addresses, CancellationToken ct)
        {
            var stateById = await LoadByIdAsync(_states, addresses.Select(a => a.StateId), ct);
            var countryById = await LoadByIdAsync(_countries, addresses.Select(a => a.CountryId), ct);

            foreach (var address in addresses)
            {
                if (stateById.TryGetValue(address.StateId, out var state))
                {
                    address.StateCode = state.Code;
                }

                if (countryById.TryGetValue(address.CountryId, out var country))
                {
                    address.CountryCode = ToAlpha2(country.Code);
                }
            }
        }

        private async Task ResolveLineItemCodesAsync(List<RateQuoteLineItem> lineItems, CancellationToken ct)
        {
            if (lineItems is null || lineItems.Count == 0)
            {
                return;
            }

            var lookup = await _constants.GetByConstantTypeIdsAsync(
                [ConstantTypeConstant.ShippingUnits, ConstantTypeConstant.FreightClass], ct);
            var constantById = lookup.ToDictionary(c => c.Id);

            foreach (var item in lineItems)
            {
                if (constantById.TryGetValue(item.ClassId, out var freightClass)
                    && freightClass.ConstantTypeId == ConstantTypeConstant.FreightClass)
                {
                    item.FreightClass = freightClass.Code;
                }

                if (constantById.TryGetValue(item.UnitId, out var shippingUnit)
                    && shippingUnit.ConstantTypeId == ConstantTypeConstant.ShippingUnits)
                {
                    item.ShippingUnitCode = shippingUnit.Code;
                }
            }
        }

        private static async Task<Dictionary<long, T>> LoadByIdAsync<T>(
            IGenericRepository<T> repository, IEnumerable<long> ids, CancellationToken ct) where T : BaseEntity
        {
            var result = new Dictionary<long, T>();
            foreach (var id in ids.Where(id => id > 0).Distinct())
            {
                var entity = await repository.GetByIdAsync(id, ct);
                if (entity is not null)
                {
                    result[id] = entity;
                }
            }

            return result;
        }

        private static string ToAlpha2(string? countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                return string.Empty;
            }

            return _alpha3ToAlpha2.TryGetValue(countryCode, out var alpha2)
                ? alpha2
                : countryCode;
        }
    }
}
