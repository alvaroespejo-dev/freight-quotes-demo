using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.States.Update;

public class UpdateStateNameHandler(IStateRepository states, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateStateNameCommand, bool>
{
    private readonly IStateRepository _states = states;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(UpdateStateNameCommand request, CancellationToken ct)
    {
        var state = await _states.GetByIdAsync(request.Id, ct);
        if (state is null)
        {
            return false;
        }

        state.Name = request.Name.Trim();

        await _states.UpdateAsync(state, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}
