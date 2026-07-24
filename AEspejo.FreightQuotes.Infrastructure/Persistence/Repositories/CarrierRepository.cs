using AEspejo.FreightQuotes.Application.Interfaces;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Common;
using Microsoft.EntityFrameworkCore;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Repositories;

public class CarrierRepository(FreightQuotesDbContext db) : GenericRepository<Carrier>(db), ICarrierRepository
{
    public async Task<IReadOnlyList<Carrier>> GetByIsActiveAsync(bool isActive, CancellationToken ct)
        => await _db.Carriers.AsNoTracking()
            .Include(c => c.Settings.Where(s => s.IsActive))
            .Where(c => c.IsActive == isActive)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<Carrier>> GetAsync(CancellationToken ct)
        => await _db.Carriers.AsNoTracking().ToListAsync(ct);

    public async Task<SearchListResponse<Carrier>> GetAsync(int pageSize, int pageNumber, string searchQuery, CancellationToken ct)
    {
        SearchListResponse<Carrier> searchListResponse = new();

        var query = _db.Carriers.Select(c => c);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            query = query.Where(c =>
                   c.Name.Contains(searchQuery)
                || c.Scac.Contains(searchQuery));
        }

        searchListResponse.TotalItems = query.Count();

        searchListResponse.Items = await query
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToListAsync();

        return searchListResponse;
    }


    public async Task DeleteCarrierAsync(long carrierId, CancellationToken ct)
    {
        var carrier = await _db.Set<Carrier>().FindAsync(carrierId);
        if (carrier != null)
        {
            _db.Set<Carrier>().Remove(carrier);
            await _db.SaveChangesAsync(ct);
        }
    }
}