using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Accessorials.Update;

public record UpdateAccessorialNameCommand(long Id, string Name) : IRequest<bool>;
