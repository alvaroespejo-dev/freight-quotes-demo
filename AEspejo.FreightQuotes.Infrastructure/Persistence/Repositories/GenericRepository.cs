using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Repositories;

public class GenericRepository<T>(FreightQuotesDbContext db) : IGenericRepository<T> where T : BaseEntity
{
    protected readonly FreightQuotesDbContext _db = db;
    protected readonly DbSet<T> _set = db.Set<T>();

    public virtual async Task<T?> GetByIdAsync(long id, CancellationToken ct)
        => await _set.FindAsync([id], ct);

    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct)
        => await _set.AsNoTracking().ToListAsync(ct);

    public virtual async Task AddAsync(T entity, CancellationToken ct)
        => await _set.AddAsync(entity, ct);

    public virtual Task UpdateAsync(T entity, CancellationToken ct)
    {
        _set.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity, CancellationToken ct)
    {
        _set.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual IQueryable<T> AsQueryable() => _set.AsQueryable();
}
