export interface CarrierResponse {
  id: number;
  name: string;
  scac: string;
  isActive: boolean;
  isMockMode: boolean;
}

export interface SaveCarrierRequest {
  name: string;
  scac: string;
  isActive: boolean;
  isMockMode: boolean;
}

export interface CarrierDialogData extends SaveCarrierRequest {
  id?: number;
  type: 'create' | 'edit';
}
