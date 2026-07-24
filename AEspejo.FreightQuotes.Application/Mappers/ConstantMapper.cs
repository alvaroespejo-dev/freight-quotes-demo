using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Constant;
using AutoMapper;

namespace AEspejo.FreightQuotes.Application.Mappers;

public class ConstantMapper : Profile
{
    public ConstantMapper()
    {
        CreateMap<Constant, ConstantResponse>().ReverseMap();
    }
}