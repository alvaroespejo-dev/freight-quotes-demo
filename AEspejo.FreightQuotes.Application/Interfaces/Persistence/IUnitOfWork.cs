using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEspejo.FreightQuotes.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IAccessorialRepository Accessorials { get; }
    IConstantRepository Constants { get; }
    IConstantTypeRepository ConstantTypes { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);
    Task BeginTransactionAsync(CancellationToken ct);
    Task CommitTransactionAsync(CancellationToken ct);
    Task RollbackTransactionAsync(CancellationToken ct);
}
