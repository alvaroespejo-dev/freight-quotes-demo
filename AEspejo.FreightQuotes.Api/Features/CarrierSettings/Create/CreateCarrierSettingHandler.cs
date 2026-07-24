using AEspejo.FreightQuotes.Application.Exceptions;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings.Create;

public class CreateCarrierSettingHandler(ICarrierSettingRepository carrierSettings, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCarrierSettingCommand, long>
{
    private readonly ICarrierSettingRepository _carrierSettings = carrierSettings;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<long> Handle(CreateCarrierSettingCommand request, CancellationToken cancellationToken)
    {
        var alreadyExists = await _carrierSettings.ExistsAsync(
            request.CarrierSetting.CarrierId,
            request.CarrierSetting.SettingTypeId,
            request.CarrierSetting.CarrierSettingTypeId,
            excludeId: null,
            cancellationToken);

        if (alreadyExists)
        {
            throw new ConflictException("A setting with this type and key already exists for this carrier.");
        }

        var carrierSetting = _mapper.Map<CarrierSetting>(request.CarrierSetting);

        await _carrierSettings.AddAsync(carrierSetting, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return carrierSetting.Id;
    }
}
