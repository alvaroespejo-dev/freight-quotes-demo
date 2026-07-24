using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Accessorials.Get;
public record GetAccessorialsQuery() : IRequest<GetAccessorialsResponse>;
