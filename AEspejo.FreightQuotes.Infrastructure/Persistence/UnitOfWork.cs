using AEspejo.FreightQuotes.Application.Interfaces.Persistence;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence;

public class UnitOfWork(
    FreightQuotesDbContext db,
    IAccessorialRepository accessorials,
    IConstantRepository constants,
    IConstantTypeRepository constantTypes,
    ICountryRepository countryTypes,
    IStateRepository stateTypes
    ) : IUnitOfWork
{
    private readonly FreightQuotesDbContext _db = db;
    private IDbContextTransaction? _currentTransaction;

    public IAccessorialRepository Accessorials { get; } = accessorials;
    public IConstantRepository Constants { get; } = constants;
    public IConstantTypeRepository ConstantTypes { get; } = constantTypes;
    public ICountryRepository Countries { get; } = countryTypes;
    public IStateRepository States { get; } = stateTypes;

    public async Task<int> SaveChangesAsync(CancellationToken ct)
        => await _db.SaveChangesAsync(ct);

    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        if (_currentTransaction != null) return;
        _currentTransaction = await _db.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransactionAsync(CancellationToken ct)
    {
        try
        {
            await _db.SaveChangesAsync(ct);
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync(ct);
            }
        }
        catch
        {
            await RollbackTransactionAsync(ct);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken ct)
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync(ct);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public void Dispose()
    {
        _db.Dispose();
        _currentTransaction?.Dispose();
    }
}