using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;

public interface IStateRepository : IGenericRepository<State>
{
    Task<IReadOnlyList<State>> GetByCountryIdAsync(long countryId, CancellationToken ct);
}