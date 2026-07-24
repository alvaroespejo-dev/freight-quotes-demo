using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Countries.Get;

public record GetCountriesQuery() : IRequest<GetCountriesResponse>;