using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Constants.Update;

public record UpdateConstantNameCommand(long Id, string Name) : IRequest<bool>;
