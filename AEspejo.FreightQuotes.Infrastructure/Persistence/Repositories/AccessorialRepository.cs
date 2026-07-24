using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Repositories;

public class AccessorialRepository(FreightQuotesDbContext db) : GenericRepository<Accessorial>(db), IAccessorialRepository
{
    public async Task<Accessorial?> GetWithTypeByIdAsync(long id, CancellationToken ct)
        => await _db.Accessorials
            .AsNoTracking()
            .Include(a => a.Type)
            .FirstOrDefaultAsync(a => a.Id == id, ct);
}