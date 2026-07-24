// Lookup responses (mirror backend DTOs)
export interface ConstantResponse {
  id: number;
  constantTypeId: number;
  name: string;
  code: string;
}

export interface AccessorialResponse {
  id: number;
  name: string;
  code: string;
  typeId: number;
}

export interface CountryResponse {
  id: number;
  name: string;
  code: string;
}

export interface StateResponse {
  id: number;
  countryId: number;
  name: string;
  code: string;
}

export interface CarrierResponse {
  id: number;
  name: string;
  scac: string;
  tokenUrl: string;
  rateUrl: string;
  user: string;
  password: string;
  key: string;
  isActive: boolean;
}

// ConstantType ids (see ConstantTypeSeed.cs)
export enum ConstantTypeId {
  ShippingUnits = 1,
  SubClass = 2,
  FreightClass = 3,
  Accessorials = 4,
  PartyAddressType = 5,
  EquipmentType = 6,
  Terms = 9,
  Role = 10
}

// Accessorial category ids (Constant ids used as Accessorial.TypeId, see ConstantSeed.cs)
export enum AccessorialCategory {
  General = 83,
  DockType = 84,
  OriginDestination = 85,
  Item = 86,
  Other = 87
}

// Rate request payload (mirror RateQuoteRequest / RateQuoteAddress / RateQuoteLineItem)
export interface RateAccessorialRequest {
  id: number;
  code: string;
  cost?: number | null;
}

export interface RateQuoteAddress {
  id: number;
  name?: string | null;
  address1?: string | null;
  address2?: string | null;
  city?: string | null;
  stateId: number;
  countryId: number;
  zip: string;
  dockTypeId?: number | null;
  appointmentRequired: boolean;
  notes?: string | null;
  accessorials: RateAccessorialRequest[];
}

export interface RateQuoteLineItem {
  qty: number;
  unitId: number;
  weight: number;
  nmfc: string;
  subClassId?: number | null;
  classId: number;
  isHazMat: boolean;
  description: string;
  shipQty: number;
  length: number;
  width: number;
  height: number;
  isStackable: boolean;
}

export interface RateQuoteRequest {
  requestId: string;
  shipDate: string;
  termsId?: number | null;
  roleId?: number | null;
  billingAddress: RateQuoteAddress;
  originAddress: RateQuoteAddress;
  destinationAddress: RateQuoteAddress;
  accessorials: RateAccessorialRequest[];
  lineItems: RateQuoteLineItem[];
  equipmentTypeId?: number | null;
  totalShippingUnits?: number | null;
  mileage?: number | null;
  includeLtlQuotes: boolean;
  includeVolumeQuotes: boolean;
  includeGuaranteedQuotes: boolean;
  includeMabdQuotes: boolean;
}

// Rate response payload (mirror RateQuoteResponse)
export interface RateAccessorialResponse {
  code: string;
  name: string;
  cost: number;
}

export interface RateQuoteResponse {
  carrierId: number;
  carrierName: string;
  quoteNumber: string;
  serviceLevel: string;
  baseCharge: number;
  accessorialCharge: number;
  totalCharge: number;
  transitDays: number;
  note: string;
  hasError: boolean;
  accessorial: RateAccessorialResponse[];
}
