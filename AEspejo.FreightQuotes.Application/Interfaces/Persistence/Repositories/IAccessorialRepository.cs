using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;

public interface IAccessorialRepository : IGenericRepository<Accessorial>
{
    Task<Accessorial?> GetWithTypeByIdAsync(long id, CancellationToken ct);
}