using AEspejo.FreightQuotes.Shared.Dtos.State;

namespace AEspejo.FreightQuotes.Api.Features.States.Get;

public record GetStatesResponse(IReadOnlyList<StateResponse> States);