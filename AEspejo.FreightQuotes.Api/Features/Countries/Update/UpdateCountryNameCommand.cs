using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Countries.Update;

public record UpdateCountryNameCommand(long Id, string Name) : IRequest<bool>;
