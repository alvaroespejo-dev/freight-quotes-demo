using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Shared.Dtos.Constant;
using AEspejo.FreightQuotes.Shared.Dtos.Country;
using AutoMapper;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Countries.Get;

public class GetCountriesHandler(ICountryRepository countries, IMapper mapper) : IRequestHandler<GetCountriesQuery, GetCountriesResponse>
{
    private readonly ICountryRepository _countries = countries;
    private readonly IMapper _mapper = mapper;

    public async Task<GetCountriesResponse> Handle(GetCountriesQuery request, CancellationToken ct)
    {
        var all = await _countries.GetAllAsync(ct);
        var dtos = _mapper.Map<List<CountryResponse>>(all);
        return new GetCountriesResponse(dtos);
    }
}
