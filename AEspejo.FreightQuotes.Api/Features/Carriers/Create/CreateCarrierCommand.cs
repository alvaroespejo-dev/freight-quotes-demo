using AEspejo.FreightQuotes.Shared.Dtos.Carrier;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Carriers.Create;

public record CreateCarrierCommand(SaveCarrierRequest Carrier) : IRequest<long>;
