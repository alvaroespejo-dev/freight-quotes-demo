// Mirrors AEspejo.FreightQuotes.Shared.Dtos.CarrierSetting

export interface CarrierSettingResponse {
  id: number;
  carrierId: number;
  settingTypeId: number;
  settingTypeName: string;
  carrierSettingTypeId: number;
  carrierSettingTypeName: string;
  value: string;
  isActive: boolean;
}

export interface SaveCarrierSettingRequest {
  carrierId: number;
  settingTypeId: number;
  carrierSettingTypeId: number;
  value: string;
  isActive: boolean;
}

export interface CarrierSettingsDialogData {
  carrierId: number;
  carrierName: string;
}

// Lookup option sourced from the /constants endpoint
export interface ConstantOption {
  id: number;
  constantTypeId: number;
  name: string;
  code: string;
}

// ConstantType ids backing the setting lookups (see ConstantTypeSeed.cs)
export enum SettingConstantTypeId {
  SettingType = 7,        // Rating / Authentication
  CarrierSettingType = 8  // URL / ClientId / ApiKey / ...
}
