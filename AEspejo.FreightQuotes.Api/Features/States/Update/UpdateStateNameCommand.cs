using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.States.Update;

public record UpdateStateNameCommand(long Id, string Name) : IRequest<bool>;
