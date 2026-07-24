using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Dtos.Common;

namespace AEspejo.FreightQuotes.Application.Interfaces;

public interface ICarrierRepository : IGenericRepository<Carrier>
{
    Task<IReadOnlyList<Carrier>> GetByIsActiveAsync(bool isActive, CancellationToken ct);
    Task<IReadOnlyList<Carrier>> GetAsync(CancellationToken ct);
    Task<SearchListResponse<Carrier>> GetAsync(int pageSize, int pageNumber, string searchQuery, CancellationToken ct);
    Task DeleteCarrierAsync(long carrierId, CancellationToken ct);
}
