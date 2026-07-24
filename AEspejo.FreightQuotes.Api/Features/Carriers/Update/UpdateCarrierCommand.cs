using AEspejo.FreightQuotes.Shared.Dtos.Carrier;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Carriers.Update;

public record UpdateCarrierCommand(long CarrierId, SaveCarrierRequest Carrier) : IRequest<bool>;
