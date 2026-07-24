namespace AEspejo.FreightQuotes.Shared.Dtos.CarrierSetting;

public record CarrierSettingResponse(
    long Id,
    long CarrierId,
    long SettingTypeId,
    string SettingTypeName,
    long CarrierSettingTypeId,
    string CarrierSettingTypeName,
    string Value,
    bool IsActive
);
