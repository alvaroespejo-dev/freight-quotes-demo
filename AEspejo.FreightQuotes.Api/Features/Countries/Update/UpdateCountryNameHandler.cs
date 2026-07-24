using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using MediatR;

namespace AEspejo.FreightQuotes.Api.Features.Countries.Update;

public class UpdateCountryNameHandler(ICountryRepository countries, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCountryNameCommand, bool>
{
    private readonly ICountryRepository _countries = countries;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(UpdateCountryNameCommand request, CancellationToken ct)
    {
        var country = await _countries.GetByIdAsync(request.Id, ct);
        if (country is null)
        {
            return false;
        }

        country.Name = request.Name.Trim();

        await _countries.UpdateAsync(country, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}
