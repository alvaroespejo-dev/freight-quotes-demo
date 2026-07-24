using AEspejo.FreightQuotes.Application.Interfaces.Persistence.Repositories;
using AEspejo.FreightQuotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Repositories;

public class CarrierSettingRepository(FreightQuotesDbContext db) : GenericRepository<CarrierSetting>(db), ICarrierSettingRepository
{
    public async Task<IReadOnlyList<CarrierSetting>> GetByCarrierIdAsync(long carrierId, CancellationToken ct)
        => await _db.CarrierSettings
                    .AsNoTracking()
                    .Include(cs => cs.SettingType)
                    .Include(cs => cs.CarrierSettingType)
                    .Where(cs => cs.CarrierId == carrierId)
                    .OrderBy(cs => cs.SettingTypeId)
                    .ThenBy(cs => cs.CarrierSettingTypeId)
                    .ToListAsync(ct);

    public async Task<bool> ExistsAsync(long carrierId, long settingTypeId, long carrierSettingTypeId, long? excludeId, CancellationToken ct)
        => await _db.CarrierSettings
                    .AsNoTracking()
                    .AnyAsync(cs => cs.CarrierId == carrierId
                                 && cs.SettingTypeId == settingTypeId
                                 && cs.CarrierSettingTypeId == carrierSettingTypeId
                                 && (excludeId == null || cs.Id != excludeId), ct);

    public async Task DeleteCarrierSettingAsync(long carrierSettingId, CancellationToken ct)
    {
        var setting = await _db.CarrierSettings.FindAsync([carrierSettingId], ct);
        if (setting != null)
        {
            _db.CarrierSettings.Remove(setting);
            await _db.SaveChangesAsync(ct);
        }
    }
}
