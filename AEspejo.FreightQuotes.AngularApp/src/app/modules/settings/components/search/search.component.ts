import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ReferenceDataService } from '../../reference.service';
import {
  AccessorialItem,
  ConstantItem,
  CountryItem,
  ReferenceConstantTypeId,
  ReferenceKind,
  StateItem,
} from '../../reference.types';

interface ConstantTab {
  label: string;
  typeId: number;
}

type AccessorialRow = AccessorialItem & { categoryName: string };
type StateRow = StateItem & { countryName: string };

type EditableRow = { id: number; name: string };

@Component({
  selector: 'app-settings-search',
  standalone: false,
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss',
})
export class SearchComponent implements OnInit {

  private svc = inject(ReferenceDataService);
  private toastr = inject(ToastrService);

  isLoading = this.svc.isLoading;
  error = this.svc.error;

  filter = signal('');
  savingId = signal<string | null>(null);

  readonly codeNameColumns = ['code', 'name', 'actions'];
  readonly accessorialColumns = ['code', 'name', 'category', 'actions'];
  readonly stateColumns = ['code', 'name', 'country', 'actions'];

  readonly constantTabs: ConstantTab[] = [
    { label: 'Freight Classes', typeId: ReferenceConstantTypeId.FreightClass },
    { label: 'Shipping Units', typeId: ReferenceConstantTypeId.ShippingUnits },
    { label: 'Equipment Types', typeId: ReferenceConstantTypeId.EquipmentType },
    { label: 'Terms', typeId: ReferenceConstantTypeId.Terms },
    { label: 'Roles', typeId: ReferenceConstantTypeId.Role },
  ];

  private constantNameById = computed(() => {
    const map = new Map<number, string>();
    for (const c of this.svc.constants()) {
      map.set(c.id, c.name);
    }
    return map;
  });

  private countryNameById = computed(() => {
    const map = new Map<number, string>();
    for (const c of this.svc.countries()) {
      map.set(c.id, c.name);
    }
    return map;
  });

  private accessorialRows = computed<AccessorialRow[]>(() =>
    this.svc.accessorials().map(a => ({ ...a, categoryName: this.constantNameById().get(a.typeId) ?? '' })));

  private stateRows = computed<StateRow[]>(() =>
    this.svc.states().map(s => ({ ...s, countryName: this.countryNameById().get(s.countryId) ?? '' })));

  ngOnInit(): void {
    void this.svc.load();
  }

  constantsFor(typeId: number): ConstantItem[] {
    return this.match(this.svc.constants().filter(c => c.constantTypeId === typeId), c => [c.code, c.name]);
  }

  accessorials(): AccessorialRow[] {
    return this.match(this.accessorialRows(), a => [a.code, a.name, a.categoryName]);
  }

  countries(): CountryItem[] {
    return this.match(this.svc.countries(), c => [c.code, c.name]);
  }

  states(): StateRow[] {
    return this.match(this.stateRows(), s => [s.code, s.name, s.countryName]);
  }

  isSaving(kind: ReferenceKind, row: EditableRow): boolean {
    return this.savingId() === this.rowKey(kind, row);
  }

  async save(kind: ReferenceKind, row: EditableRow): Promise<void> {
    const name = (row.name ?? '').trim();
    if (!name) {
      this.toastr.warning('Name is required', 'Validation');
      return;
    }

    this.savingId.set(this.rowKey(kind, row));
    try {
      await this.svc.updateName(kind, row.id, name);
      await this.svc.load();
      this.toastr.success('Name updated successfully', 'Success');
    } catch {
      this.toastr.error('Unable to update name', 'Error');
    } finally {
      this.savingId.set(null);
    }
  }

  private rowKey(kind: ReferenceKind, row: EditableRow): string {
    return `${kind}-${row.id}`;
  }

  private match<T>(items: T[], fields: (item: T) => string[]): T[] {
    const term = this.filter().trim().toLowerCase();
    if (!term) {
      return items;
    }
    return items.filter(i => fields(i).some(f => (f ?? '').toLowerCase().includes(term)));
  }
}
