using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(long id, CancellationToken ct);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct);
    Task AddAsync(T entity, CancellationToken ct);
    Task UpdateAsync(T entity, CancellationToken ct);
    Task DeleteAsync(T entity, CancellationToken ct);
    IQueryable<T> AsQueryable();
}
