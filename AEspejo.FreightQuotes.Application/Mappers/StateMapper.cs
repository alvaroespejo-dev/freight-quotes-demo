using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.State;
using AutoMapper;

namespace AEspejo.FreightQuotes.Application.Mappers;

public class StateMapper : Profile
{
    public StateMapper()
    {
        CreateMap<State, StateResponse>().ReverseMap();
    }
}
