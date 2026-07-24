using AEspejo.FreightQuotes.Application.Interfaces;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Carriers.Delete;

public class DeleteCarrierHandler(ICarrierRepository carrierRepository) : IRequestHandler<DeleteCarrierCommand>
{
    private readonly ICarrierRepository _carrierRepository = carrierRepository;

    public async Task Handle(DeleteCarrierCommand request, CancellationToken cancellationToken)
    {
        await _carrierRepository.DeleteCarrierAsync(request.CarrierId, cancellationToken);
    }
}
