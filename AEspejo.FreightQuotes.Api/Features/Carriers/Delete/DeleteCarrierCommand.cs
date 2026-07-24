using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Carriers.Delete;

public record DeleteCarrierCommand(long CarrierId) : IRequest;

