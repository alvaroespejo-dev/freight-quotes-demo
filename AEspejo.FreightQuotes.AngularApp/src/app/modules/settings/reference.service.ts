import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ConfigService } from '../../services/config.service';
import {
  AccessorialItem,
  ConstantItem,
  CountryItem,
  ReferenceConstantTypeId,
  ReferenceKind,
  StateItem,
} from './reference.types';

/**
 * Loads the read-only reference/catalog data shown on the /settings page from the existing
 * lookup endpoints (/constants, /accessorials, /countries, /states). State is held in signals,
 * consistent with the other feature services.
 */
@Injectable({ providedIn: 'root' })
export class ReferenceDataService {

  private httpClient = inject(HttpClient);
  private config = inject(ConfigService);

  // Constant types surfaced in the catalog (freight classes, shipping units, equipment, terms,
  // roles) plus accessorial categories (type 4) needed to label the accessorials tab.
  private static readonly ConstantTypeIds = [
    ReferenceConstantTypeId.ShippingUnits,
    ReferenceConstantTypeId.FreightClass,
    ReferenceConstantTypeId.AccessorialCategory,
    ReferenceConstantTypeId.EquipmentType,
    ReferenceConstantTypeId.Terms,
    ReferenceConstantTypeId.Role,
  ];

  private constantsSignal = signal<ConstantItem[]>([]);
  private accessorialsSignal = signal<AccessorialItem[]>([]);
  private countriesSignal = signal<CountryItem[]>([]);
  private statesSignal = signal<StateItem[]>([]);
  private loadingSignal = signal(false);
  private errorSignal = signal<string | null>(null);

  constants = this.constantsSignal.asReadonly();
  accessorials = this.accessorialsSignal.asReadonly();
  countries = this.countriesSignal.asReadonly();
  states = this.statesSignal.asReadonly();
  isLoading = this.loadingSignal.asReadonly();
  error = this.errorSignal.asReadonly();

  /** Persists a new Name for a reference entity. `kind` maps directly to the API route. */
  async updateName(kind: ReferenceKind, id: number, name: string): Promise<void> {
    await firstValueFrom(
      this.httpClient.put<void>(`${this.config.getApiUrl()}/${kind}/${id}`, { name })
    );
  }

  /** Loads every catalog list in parallel. */
  async load(): Promise<void> {
    this.loadingSignal.set(true);
    this.errorSignal.set(null);

    try {
      const api = this.config.getApiUrl();

      let params = new HttpParams();
      ReferenceDataService.ConstantTypeIds.forEach(id => (params = params.append('constantTypeIds', id)));

      const [constants, accessorials, countries, states] = await Promise.all([
        firstValueFrom(this.httpClient.get<{ constants: ConstantItem[] }>(`${api}/constants`, { params })),
        firstValueFrom(this.httpClient.get<{ accessorials: AccessorialItem[] }>(`${api}/accessorials`)),
        firstValueFrom(this.httpClient.get<{ countries: CountryItem[] }>(`${api}/countries`)),
        firstValueFrom(this.httpClient.get<{ states: StateItem[] }>(`${api}/states`)),
      ]);

      this.constantsSignal.set(constants.constants ?? []);
      this.accessorialsSignal.set(accessorials.accessorials ?? []);
      this.countriesSignal.set(countries.countries ?? []);
      this.statesSignal.set(states.states ?? []);
    } catch (error) {
      this.errorSignal.set('Error loading reference data');
      console.error('Error loading reference data:', error);
    } finally {
      this.loadingSignal.set(false);
    }
  }
}
