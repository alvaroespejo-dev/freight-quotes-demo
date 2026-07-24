using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Shared.Dtos.CarrierSetting;
using AutoMapper;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings.Get;

public class GetCarrierSettingsHandler(ICarrierSettingRepository carrierSettings, IMapper mapper) : IRequestHandler<GetCarrierSettingsQuery, GetCarrierSettingsResponse>
{
    private readonly ICarrierSettingRepository _carrierSettings = carrierSettings;
    private readonly IMapper _mapper = mapper;

    public async Task<GetCarrierSettingsResponse> Handle(GetCarrierSettingsQuery request, CancellationToken ct)
    {
        var all = await _carrierSettings.GetByCarrierIdAsync(request.CarrierId, ct);
        var dtos = _mapper.Map<List<CarrierSettingResponse>>(all);
        return new GetCarrierSettingsResponse(dtos);
    }
}
