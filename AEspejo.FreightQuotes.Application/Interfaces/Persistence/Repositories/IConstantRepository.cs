using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;

public interface IConstantRepository : IGenericRepository<Constant> {
    Task<IReadOnlyList<Constant>> GetByConstantTypeIdsAsync(IEnumerable<long> constantTypeIds, CancellationToken ct);
}
