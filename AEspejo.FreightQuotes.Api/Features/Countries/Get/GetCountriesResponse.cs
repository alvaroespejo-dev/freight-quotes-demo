using AEspejo.FreightQuotes.Shared.Dtos.Country;

namespace AEspejo.FreightQuotes.Api.Features.Countries.Get;

public record GetCountriesResponse(IReadOnlyList<CountryResponse> Countries);
