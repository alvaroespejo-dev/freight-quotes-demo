using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Repositories;

public class StateRepository(FreightQuotesDbContext db) : GenericRepository<State>(db), IStateRepository
{
    public async Task<IReadOnlyList<State>> GetByCountryIdAsync(long countryId, CancellationToken ct)
    {
        return await _db.Set<State>()
            .Where(s => s.CountryId == countryId)
            .ToListAsync(ct);
    }
}