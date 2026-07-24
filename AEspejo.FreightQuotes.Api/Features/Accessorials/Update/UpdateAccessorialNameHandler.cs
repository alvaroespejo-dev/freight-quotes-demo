using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Accessorials.Update;

public class UpdateAccessorialNameHandler(IAccessorialRepository accessorials, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAccessorialNameCommand, bool>
{
    private readonly IAccessorialRepository _accessorials = accessorials;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(UpdateAccessorialNameCommand request, CancellationToken ct)
    {
        var accessorial = await _accessorials.GetByIdAsync(request.Id, ct);
        if (accessorial is null)
        {
            return false;
        }

        accessorial.Name = request.Name.Trim();

        await _accessorials.UpdateAsync(accessorial, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}
