using AEspejo.FreightQuotes.Api.Features.Constants.Get;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Carriers.Get;

public record GetCarriersQuery() : IRequest<GetCarriersResponse>;
