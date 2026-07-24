namespace AEspejo.FreightQuotes.Shared.Dtos.CarrierSetting;

public record SaveCarrierSettingRequest(
    long CarrierId = 0,
    long SettingTypeId = 0,
    long CarrierSettingTypeId = 0,
    string Value = "",
    bool IsActive = true
);
