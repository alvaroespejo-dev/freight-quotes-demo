using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.States.Get;

public record GetStatesQuery(long? CountryId = null) : IRequest<GetStatesResponse>;