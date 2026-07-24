using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Carriers.Update;

public class UpdateCarrierHandler(ICarrierRepository carrierRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCarrierCommand, bool>
{
    private readonly ICarrierRepository _carrierRepository = carrierRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(UpdateCarrierCommand request, CancellationToken cancellationToken)
    {
        var carrier = await _carrierRepository.GetByIdAsync(request.CarrierId, cancellationToken);
        if (carrier is null)
        {
            return false;
        }

        carrier.Name = request.Carrier.Name;
        carrier.Scac = request.Carrier.Scac;
        carrier.IsActive = request.Carrier.IsActive;
        carrier.IsMockMode = request.Carrier.IsMockMode;

        await _carrierRepository.UpdateAsync(carrier, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
