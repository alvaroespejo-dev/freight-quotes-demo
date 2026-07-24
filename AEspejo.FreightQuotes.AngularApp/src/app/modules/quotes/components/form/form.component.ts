import { Component, OnInit, OnDestroy, computed, inject } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { provideNativeDateAdapter } from '@angular/material/core';
import { ToastrService } from 'ngx-toastr';
import { QuotesService } from '../../quotes.service';
import {
  AccessorialCategory,
  ConstantTypeId,
  RateAccessorialRequest,
  RateQuoteAddress,
  RateQuoteRequest,
  StateResponse
} from '../../quotes.types';

/** Validators.min(0) still accepts an empty load, so require strictly positive. */
function positiveWeight(control: AbstractControl): ValidationErrors | null {
  return Number(control.value) > 0 ? null : { positiveWeight: true };
}

/** A rate request needs at least one quote type to ask for. */
function atLeastOneQuoteType(group: AbstractControl): ValidationErrors | null {
  return Object.values(group.value ?? {}).some(Boolean) ? null : { noQuoteType: true };
}

/** Sample shipments the "Sample" buttons load, resolved against the lookups at click time. */
interface ExampleAddress {
  name: string;
  address1: string;
  city: string;
  countryCode: string;
  stateCode: string;
  zip: string;
}

interface Example {
  label: string;
  billing: ExampleAddress;
  origin: ExampleAddress;
  destination: ExampleAddress;
}

@Component({
  selector: 'app-quotes-form',
  standalone: false,
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss',
  providers: [provideNativeDateAdapter()]
})
export class FormComponent implements OnInit, OnDestroy {

  private fb = inject(FormBuilder);
  quotesService = inject(QuotesService);
  private toastr = inject(ToastrService);

  form!: FormGroup;

  // Lookups derived from the service signals
  units = computed(() => this.quotesService.constants().filter(c => c.constantTypeId === ConstantTypeId.ShippingUnits));
  subClasses = computed(() => this.quotesService.constants().filter(c => c.constantTypeId === ConstantTypeId.SubClass));
  classes = computed(() => this.quotesService.constants().filter(c => c.constantTypeId === ConstantTypeId.FreightClass));
  equipmentTypes = computed(() => this.quotesService.constants().filter(c => c.constantTypeId === ConstantTypeId.EquipmentType));
  terms = computed(() => this.quotesService.constants().filter(c => c.constantTypeId === ConstantTypeId.Terms));
  roles = computed(() => this.quotesService.constants().filter(c => c.constantTypeId === ConstantTypeId.Role));
  dockTypes = computed(() => this.quotesService.accessorials().filter(a => a.typeId === AccessorialCategory.DockType));

  quoteColumns = ['carrier', 'quote', 'service', 'baseRate', 'accessCharge', 'totalCharge', 'transit', 'notes'];

  readonly addressPanels = [
    { key: 'billingAddress', label: 'Billing' },
    { key: 'originAddress', label: 'Origin' },
    { key: 'destinationAddress', label: 'Destination' }
  ];

  readonly examples: Example[] = [
    {
      label: 'Sample: US Domestic',
      billing: { name: 'Acme Corp', address1: '500 W Madison St', city: 'Chicago', countryCode: 'USA', stateCode: 'IL', zip: '60661' },
      origin: { name: 'Acme West Warehouse', address1: '1234 S Alameda St', city: 'Los Angeles', countryCode: 'USA', stateCode: 'CA', zip: '90001' },
      destination: { name: 'Houston Distribution Center', address1: '800 Bell St', city: 'Houston', countryCode: 'USA', stateCode: 'TX', zip: '77002' }
    },
    {
      label: 'Sample: Cross-Border',
      billing: { name: 'Acme Corp', address1: '500 W Madison St', city: 'Chicago', countryCode: 'USA', stateCode: 'IL', zip: '60661' },
      origin: { name: 'Acme Canada Plant', address1: '10180 101 St NW', city: 'Edmonton', countryCode: 'CAN', stateCode: 'AB', zip: 'T5J 0N3' },
      destination: { name: 'Acme West Warehouse', address1: '1234 S Alameda St', city: 'Los Angeles', countryCode: 'USA', stateCode: 'CA', zip: '90001' }
    }
  ];

  /** Which address panels are open; kept in sync with the user's own toggling. */
  private expandedPanels = new Set<string>();

  isExpanded(key: string): boolean {
    return this.expandedPanels.has(key);
  }

  setExpanded(key: string, expanded: boolean): void {
    expanded ? this.expandedPanels.add(key) : this.expandedPanels.delete(key);
  }

  ngOnInit(): void {
    this.buildForm();
    this.loadData();
  }

  private buildForm(): void {
    this.form = this.fb.group({
      shipDate: [new Date(), Validators.required],
      termsId: [this.defaultTermsId()],
      roleId: [this.defaultRoleId()],
      billingAddress: this.buildAddressGroup(false),
      originAddress: this.buildAddressGroup(true),
      destinationAddress: this.buildAddressGroup(true),
      // shipment-level accessorials
      doNotFreeze: [false],
      insuranceRequired: [false],
      cod: [false],
      inBond: [false],
      // shipping units / equipment
      equipmentTypeId: [null],
      totalShippingUnits: [null],
      mileage: [null],
      // rate quote type — at least one must stay selected
      rateQuoteType: this.fb.group({
        includeLtlQuotes: [true],
        includeVolumeQuotes: [false],
        includeGuaranteedQuotes: [false],
        includeMabdQuotes: [false]
      }, { validators: atLeastOneQuoteType }),
      lineItems: this.fb.array([this.buildLineItem()])
    });
  }

  /** Resets every field back to how the form opens. */
  clearForm(): void {
    this.buildForm();
    this.applyDefaultCountry();
    this.applyLineItemDefaults();
    this.applyTermsRoleDefaults();
    this.expandedPanels.clear();
    this.quotesService.reset();
  }

  ngOnDestroy(): void {
    this.quotesService.reset();
  }

  private async loadData(): Promise<void> {
    await Promise.all([
      this.quotesService.loadLookups(),
      this.quotesService.loadStates()
    ]);
    this.applyDefaultCountry();
    this.applyLineItemDefaults();
    this.applyTermsRoleDefaults();
  }

  private applyDefaultCountry(): void {
    const usa = this.quotesService.countries().find(c => c.code === 'USA' || c.code === 'US' || c.name === 'USA');
    if (usa) {
      ['billingAddress', 'originAddress', 'destinationAddress'].forEach(key => {
        const group = this.form.get(key) as FormGroup;
        if (!group.get('countryId')!.value) {
          group.get('countryId')!.setValue(usa.id);
        }
      });
    }
  }

  private buildAddressGroup(withServices: boolean): FormGroup {
    const group: Record<string, any> = {
      name: [''],
      address1: [''],
      address2: [''],
      city: [''],
      stateId: [null],
      countryId: [null],
      zip: ['', Validators.required]
    };
    if (withServices) {
      group['dockTypeId'] = [null];
      group['appointmentRequired'] = [false];
      group['notes'] = [''];
      group['liftGate'] = [false];
      group['inside'] = [false];
    }
    return this.fb.group(group);
  }

  /** Lookups load after the form is built, so these are null on the first pass
      and backfilled by applyLineItemDefaults(); "Add New" rows resolve directly. */
  private defaultUnitId(): number | null {
    return this.units().find(u => u.name === 'Pallets')?.id ?? null;
  }

  private defaultClassId(): number | null {
    return this.classes().find(c => c.name === '50')?.id ?? null;
  }

  private defaultTermsId(): number | null {
    return this.terms().find(t => t.name === 'Prepaid')?.id ?? null;
  }

  private defaultRoleId(): number | null {
    return this.roles().find(r => r.name === 'Shipper')?.id ?? null;
  }

  private applyTermsRoleDefaults(): void {
    if (!this.form.get('termsId')!.value) this.form.get('termsId')!.setValue(this.defaultTermsId());
    if (!this.form.get('roleId')!.value) this.form.get('roleId')!.setValue(this.defaultRoleId());
  }

  private applyLineItemDefaults(): void {
    this.lineItems.controls.forEach(ctrl => {
      if (!ctrl.get('unitId')!.value) ctrl.get('unitId')!.setValue(this.defaultUnitId());
      if (!ctrl.get('classId')!.value) ctrl.get('classId')!.setValue(this.defaultClassId());
    });
  }

  private buildLineItem(): FormGroup {
    return this.fb.group({
      qty: [1, [Validators.required, Validators.min(1)]],
      unitId: [this.defaultUnitId(), Validators.required],
      weight: [0, [Validators.required, positiveWeight]],
      nmfc: [''],
      subClassId: [null],
      classId: [this.defaultClassId(), Validators.required],
      isHazMat: [false],
      description: [''],
      length: [0],
      width: [0],
      height: [0],
      isStackable: [true]
    });
  }

  get lineItems(): FormArray {
    return this.form.get('lineItems') as FormArray;
  }

  addLineItem(): void {
    this.lineItems.push(this.buildLineItem());
  }

  removeLineItem(index: number): void {
    if (this.lineItems.length > 1) {
      this.lineItems.removeAt(index);
    }
  }

  totalWeight(): number {
    return this.lineItems.controls.reduce((sum, ctrl) => sum + (Number(ctrl.get('weight')!.value) || 0), 0);
  }

  /** States belonging to the given country; each address filters independently. */
  statesFor(countryId: number | null | undefined): StateResponse[] {
    if (!countryId) return [];
    return this.quotesService.states().filter(s => s.countryId === countryId);
  }

  onCountryChange(group: FormGroup): void {
    const stateCtrl = group.get('stateId')!;
    const countryId = group.get('countryId')!.value;
    // Drop a state that no longer belongs to the selected country.
    if (stateCtrl.value && !this.statesFor(countryId).some(s => s.id === stateCtrl.value)) {
      stateCtrl.setValue(null);
    }
  }

  private toDateString(value: Date | string): string {
    if (!value) return '';
    const date = value instanceof Date ? value : new Date(value);
    // Local yyyy-MM-dd to avoid timezone shifts.
    const y = date.getFullYear();
    const m = String(date.getMonth() + 1).padStart(2, '0');
    const d = String(date.getDate()).padStart(2, '0');
    return `${y}-${m}-${d}`;
  }

  // --- Collapsed address summary helpers ---
  stateName(id: number): string {
    return this.quotesService.states().find(s => s.id === id)?.name ?? '';
  }

  countryName(id: number): string {
    return this.quotesService.countries().find(c => c.id === id)?.name ?? '';
  }

  dockTypeName(id: number): string {
    return this.quotesService.accessorials().find(a => a.id === id)?.name ?? '';
  }

  equipmentTypeName(id: number): string {
    return this.equipmentTypes().find(e => e.id === id)?.name ?? '';
  }

  accessorialsSummaryItems(): string[] {
    const items: string[] = [];
    if (this.form.get('doNotFreeze')?.value) items.push('Do Not Freeze');
    if (this.form.get('insuranceRequired')?.value) items.push('Insurance Required');
    if (this.form.get('cod')?.value) items.push('COD');
    if (this.form.get('inBond')?.value) items.push('In Bond');
    return items;
  }

  shippingSummaryItems(): { label: string; value: string }[] {
    const items: { label: string; value: string }[] = [];
    const push = (label: string, value: any) => {
      if (value !== null && value !== undefined && value !== '') {
        items.push({ label, value: String(value) });
      }
    };
    push('Total Units', this.form.get('totalShippingUnits')?.value);
    push('Equipment', this.equipmentTypeName(this.form.get('equipmentTypeId')?.value));
    push('Mileage', this.form.get('mileage')?.value);
    return items;
  }

  quotesSummaryItems(): string[] {
    const group = this.form.get('rateQuoteType')!;
    const labels: Record<string, string> = {
      includeLtlQuotes: 'LTL',
      includeVolumeQuotes: 'Volume',
      includeGuaranteedQuotes: 'Guaranteed',
      includeMabdQuotes: 'MABD'
    };
    return Object.keys(labels).filter(key => group.get(key)!.value).map(key => labels[key]);
  }

  get rateQuoteTypeInvalid(): boolean {
    const group = this.form.get('rateQuoteType')!;
    return group.invalid && group.touched;
  }

  // --- Results helpers ---
  private minTotal(): number | null {
    const valid = this.quotesService.quotes().filter(q => !q.hasError);
    if (!valid.length) return null;
    return Math.min(...valid.map(q => q.totalCharge));
  }

  isBestRate(q: { hasError: boolean; totalCharge: number }): boolean {
    const min = this.minTotal();
    return min !== null && !q.hasError && q.totalCharge === min && this.quotesService.quotes().length > 1;
  }

  addressSummaryItems(group: FormGroup, services: boolean): { label: string; value: string }[] {
    const items: { label: string; value: string }[] = [];
    const push = (label: string, value: any) => {
      if (value !== null && value !== undefined && value !== '') {
        items.push({ label, value: String(value) });
      }
    };
    push('Name', group.get('name')?.value);
    push('Address', group.get('address1')?.value);
    push('Address 2', group.get('address2')?.value);
    push('City', group.get('city')?.value);
    push('State', this.stateName(group.get('stateId')?.value));
    push('Zip', group.get('zip')?.value);
    push('Country', this.countryName(group.get('countryId')?.value));
    if (services) {
      push('Dock Type', this.dockTypeName(group.get('dockTypeId')?.value));
      if (group.get('appointmentRequired')?.value) push('Appointment', 'Yes');
      push('Notes', group.get('notes')?.value);
      if (group.get('liftGate')?.value) push('Lift Gate', 'Yes');
      if (group.get('inside')?.value) push('Inside', 'Yes');
    }
    return items;
  }

  private accessorialByCode(code: string): RateAccessorialRequest | null {
    const found = this.quotesService.accessorials().find(a => a.code === code);
    return found ? { id: found.id, code: found.code } : null;
  }

  private mapAddress(key: string, withServices: boolean): RateQuoteAddress {
    const g = this.form.get(key) as FormGroup;
    const accessorials: RateAccessorialRequest[] = [];
    if (withServices) {
      if (g.get('liftGate')!.value) {
        const a = this.accessorialByCode('LFG');
        if (a) accessorials.push(a);
      }
      if (g.get('inside')!.value) {
        const a = this.accessorialByCode('INP');
        if (a) accessorials.push(a);
      }
    }
    return {
      id: 0,
      name: g.get('name')!.value,
      address1: g.get('address1')!.value,
      address2: g.get('address2')!.value,
      city: g.get('city')!.value,
      stateId: g.get('stateId')!.value ?? 0,
      countryId: g.get('countryId')!.value ?? 0,
      zip: g.get('zip')!.value ?? '',
      dockTypeId: withServices ? g.get('dockTypeId')!.value : null,
      appointmentRequired: withServices ? !!g.get('appointmentRequired')!.value : false,
      notes: withServices ? g.get('notes')!.value : null,
      accessorials
    };
  }

  private mapShipmentAccessorials(): RateAccessorialRequest[] {
    const result: RateAccessorialRequest[] = [];
    const map: { control: string; code: string }[] = [
      { control: 'doNotFreeze', code: 'PFF' },
      { control: 'insuranceRequired', code: 'INS' },
      { control: 'cod', code: 'COD' },
      { control: 'inBond', code: 'INB' }
    ];
    map.forEach(({ control, code }) => {
      if (this.form.get(control)!.value) {
        const a = this.accessorialByCode(code);
        if (a) result.push(a);
      }
    });
    return result;
  }

  /** Fills the form with a sample shipment so a rate can be requested in one click. */
  applyExample(index: number): void {
    const example = this.examples[index];
    this.addressPanels.forEach(({ key }) => this.expandedPanels.delete(key));
    this.form.patchValue({
      termsId: this.defaultTermsId(),
      roleId: this.defaultRoleId()
    });
    this.fillAddress('billingAddress', example.billing);
    this.fillAddress('originAddress', example.origin);
    this.fillAddress('destinationAddress', example.destination);

    while (this.lineItems.length > 1) {
      this.lineItems.removeAt(1);
    }
    this.lineItems.at(0).patchValue({
      qty: 1,
      unitId: this.defaultUnitId(),
      weight: 900,
      classId: this.defaultClassId(),
      description: 'test'
    });
  }

  private fillAddress(key: string, address: ExampleAddress): void {
    const country = this.quotesService.countries().find(c => c.code === address.countryCode);
    const state = this.quotesService.states().find(s => s.countryId === country?.id && s.code === address.stateCode);
    this.form.get(key)!.patchValue({
      name: address.name,
      address1: address.address1,
      address2: '',
      city: address.city,
      countryId: country?.id ?? null,
      stateId: state?.id ?? null,
      zip: address.zip
    });
  }

  /** Names the invalid controls so the user knows what to fix. */
  private missingFields(): string[] {
    const missing: string[] = [];

    if (this.form.get('shipDate')!.invalid) {
      missing.push('Ship Date');
    }

    this.addressPanels.forEach(({ key, label }) => {
      if (this.form.get(key)!.get('zip')!.invalid) {
        missing.push(`${label}: Zip`);
      }
    });

    if (this.form.get('rateQuoteType')!.invalid) {
      missing.push('Rate Quote Type: pick at least one');
    }

    const labels: Record<string, string> = {
      qty: 'QTY',
      unitId: 'Units',
      weight: 'Wgt (lbs)',
      classId: 'Class'
    };
    this.lineItems.controls.forEach((ctrl, i) => {
      const invalid = Object.keys(labels).filter(key => ctrl.get(key)!.invalid);
      if (invalid.length) {
        missing.push(`Load item ${i + 1}: ${invalid.map(k => labels[k]).join(', ')}`);
      }
    });

    return missing;
  }

  async submit(): Promise<void> {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      // Required fields sit inside collapsed panels; open the offending ones.
      [...this.addressPanels.map(p => p.key), 'rateQuoteType'].forEach(key => {
        if (this.form.get(key)!.invalid) {
          this.expandedPanels.add(key);
        }
      });
      const missing = this.missingFields();
      this.toastr.warning(
        missing.length ? `Complete: ${missing.join(' | ')}` : 'Please complete the required fields',
        'Incomplete form'
      );
      return;
    }

    const v = this.form.value;
    const request: RateQuoteRequest = {
      requestId: this.newRequestId(),
      shipDate: this.toDateString(v.shipDate),
      termsId: v.termsId,
      roleId: v.roleId,
      billingAddress: this.mapAddress('billingAddress', false),
      originAddress: this.mapAddress('originAddress', true),
      destinationAddress: this.mapAddress('destinationAddress', true),
      accessorials: this.mapShipmentAccessorials(),
      lineItems: this.lineItems.controls.map(ctrl => {
        const l = ctrl.value;
        return {
          qty: Number(l.qty),
          unitId: l.unitId,
          weight: Number(l.weight),
          nmfc: l.nmfc ?? '',
          subClassId: l.subClassId,
          classId: l.classId,
          isHazMat: !!l.isHazMat,
          description: l.description ?? '',
          shipQty: Number(l.qty),
          length: Number(l.length),
          width: Number(l.width),
          height: Number(l.height),
          isStackable: !!l.isStackable
        };
      }),
      equipmentTypeId: v.equipmentTypeId,
      totalShippingUnits: v.totalShippingUnits,
      mileage: v.mileage,
      includeLtlQuotes: !!v.rateQuoteType.includeLtlQuotes,
      includeVolumeQuotes: !!v.rateQuoteType.includeVolumeQuotes,
      includeGuaranteedQuotes: !!v.rateQuoteType.includeGuaranteedQuotes,
      includeMabdQuotes: !!v.rateQuoteType.includeMabdQuotes
    };

    await this.quotesService.requestQuotes(request);
  }

  private newRequestId(): string {
    if (typeof crypto !== 'undefined' && 'randomUUID' in crypto) {
      return crypto.randomUUID();
    }
    return 'req-' + Date.now() + '-' + Math.random().toString(16).slice(2);
  }
}
