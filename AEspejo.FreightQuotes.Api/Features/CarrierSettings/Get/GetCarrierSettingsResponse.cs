using AEspejo.FreightQuotes.Shared.Dtos.CarrierSetting;

namespace AEspejo.FreightQuotes.Api.Features.CarrierSettings.Get;

public record GetCarrierSettingsResponse(IReadOnlyList<CarrierSettingResponse> CarrierSettings);
