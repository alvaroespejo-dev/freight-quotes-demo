using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Constants.Update;

public class UpdateConstantNameHandler(IConstantRepository constants, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateConstantNameCommand, bool>
{
    private readonly IConstantRepository _constants = constants;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(UpdateConstantNameCommand request, CancellationToken ct)
    {
        var constant = await _constants.GetByIdAsync(request.Id, ct);
        if (constant is null)
        {
            return false;
        }

        constant.Name = request.Name.Trim();

        await _constants.UpdateAsync(constant, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}
