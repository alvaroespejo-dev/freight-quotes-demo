using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.CarrierSetting;
using AutoMapper;

namespace AEspejo.FreightQuotes.Application.Mappers;

public class CarrierSettingMapper : Profile
{
    public CarrierSettingMapper()
    {
        CreateMap<CarrierSetting, CarrierSettingResponse>()
            .ForMember(d => d.SettingTypeName, o => o.MapFrom(s => s.SettingType.Name))
            .ForMember(d => d.CarrierSettingTypeName, o => o.MapFrom(s => s.CarrierSettingType.Name));

        CreateMap<SaveCarrierSettingRequest, CarrierSetting>();
    }
}
