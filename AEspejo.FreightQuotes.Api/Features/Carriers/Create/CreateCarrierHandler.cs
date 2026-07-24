using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using AEspejo.FreightQuotes.Domain.Entities;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Carriers.Create;

public class CreateCarrierHandler(ICarrierRepository carrierRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCarrierCommand, long>
{
    private readonly ICarrierRepository _carrierRepository = carrierRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<long> Handle(CreateCarrierCommand request, CancellationToken cancellationToken)
    {
        var carrier = new Carrier
        {
            Name = request.Carrier.Name,
            Scac = request.Carrier.Scac,
            IsActive = request.Carrier.IsActive,
            IsMockMode = request.Carrier.IsMockMode
        };

        await _carrierRepository.AddAsync(carrier, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return carrier.Id;
    }
}
