// Read-only reference/catalog data surfaced on the /settings page.
// Mirrors the backend lookup DTOs (see /constants, /accessorials, /countries, /states).

export interface ConstantItem {
  id: number;
  constantTypeId: number;
  name: string;
  code: string;
}

export interface AccessorialItem {
  id: number;
  name: string;
  code: string;
  typeId: number;
}

export interface CountryItem {
  id: number;
  name: string;
  code: string;
}

export interface StateItem {
  id: number;
  countryId: number;
  name: string;
  code: string;
}

// API route segment for the editable reference lists (matches the controller routes).
export type ReferenceKind = 'constants' | 'accessorials' | 'countries' | 'states';

// ConstantType ids surfaced in the catalog (see ConstantTypeSeed.cs).
export enum ReferenceConstantTypeId {
  ShippingUnits = 1,
  FreightClass = 3,
  AccessorialCategory = 4,
  EquipmentType = 6,
  Terms = 9,
  Role = 10,
}
