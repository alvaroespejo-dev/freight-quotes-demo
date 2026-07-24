using AEspejo.FreightQuotes.Domain.Entities;

namespace AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;

public interface ICarrierSettingRepository : IGenericRepository<CarrierSetting>
{
    Task<IReadOnlyList<CarrierSetting>> GetByCarrierIdAsync(long carrierId, CancellationToken ct);
    Task<bool> ExistsAsync(long carrierId, long settingTypeId, long carrierSettingTypeId, long? excludeId, CancellationToken ct);
    Task DeleteCarrierSettingAsync(long carrierSettingId, CancellationToken ct);
}
