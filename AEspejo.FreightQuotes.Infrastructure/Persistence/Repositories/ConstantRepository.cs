using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Repositories;

public class ConstantRepository(FreightQuotesDbContext db) : GenericRepository<Constant>(db), IConstantRepository
{
    public async Task<IReadOnlyList<Constant>> GetByConstantTypeIdsAsync(IEnumerable<long> constantTypeIds, CancellationToken ct)
    {
        if (constantTypeIds == null || !constantTypeIds.Any())
            return [];

        return await _db.Constants
                      .AsNoTracking() 
                      .Where(c => constantTypeIds.Contains(c.ConstantTypeId))
                      .OrderBy(c => c.ConstantTypeId)
                      .ThenBy(c => c.Order)
                      .ThenBy(c => c.Name)
                      .ToListAsync(ct);
    }
}
