import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ConfigService } from '../../services/config.service';
import {
  CarrierSettingResponse,
  ConstantOption,
  SaveCarrierSettingRequest,
  SettingConstantTypeId
} from './carrier-setting.types';

@Injectable({
  providedIn: 'root'
})
export class CarrierSettingService {

  private httpClient = inject(HttpClient);
  private config = inject(ConfigService);

  // Signals
  private settingsSignal = signal<CarrierSettingResponse[]>([]);
  private settingTypesSignal = signal<ConstantOption[]>([]);
  private settingKeysSignal = signal<ConstantOption[]>([]);
  private loadingSignal = signal(false);
  private errorSignal = signal<string | null>(null);

  // Getters
  settings = this.settingsSignal.asReadonly();
  settingTypes = this.settingTypesSignal.asReadonly();
  settingKeys = this.settingKeysSignal.asReadonly();
  isLoading = this.loadingSignal.asReadonly();
  error = this.errorSignal.asReadonly();

  /** Loads the settings for a carrier plus the lookup options for the add form. */
  async load(carrierId: number): Promise<void> {
    this.loadingSignal.set(true);
    this.errorSignal.set(null);

    try {
      const api = this.config.getApiUrl();

      let lookupParams = new HttpParams()
        .append('constantTypeIds', SettingConstantTypeId.SettingType)
        .append('constantTypeIds', SettingConstantTypeId.CarrierSettingType);

      const [settings, constants] = await Promise.all([
        firstValueFrom(
          this.httpClient.get<{ carrierSettings: CarrierSettingResponse[] }>(
            `${api}/carriersettings`, { params: new HttpParams().set('carrierId', carrierId) })
        ),
        firstValueFrom(
          this.httpClient.get<{ constants: ConstantOption[] }>(`${api}/constants`, { params: lookupParams })
        )
      ]);

      this.settingsSignal.set(settings.carrierSettings ?? []);
      const options = constants.constants ?? [];
      this.settingTypesSignal.set(options.filter(c => c.constantTypeId === SettingConstantTypeId.SettingType));
      this.settingKeysSignal.set(options.filter(c => c.constantTypeId === SettingConstantTypeId.CarrierSettingType));
    } catch (error) {
      this.errorSignal.set('Error loading carrier settings');
      console.error('Error /carriersettings:', error);
      this.settingsSignal.set([]);
    } finally {
      this.loadingSignal.set(false);
    }
  }

  async create(request: SaveCarrierSettingRequest): Promise<number> {
    this.errorSignal.set(null);
    try {
      return await firstValueFrom(
        this.httpClient.post<number>(`${this.config.getApiUrl()}/carriersettings`, request)
      );
    } catch (error) {
      this.errorSignal.set('Error creating carrier setting');
      console.error('Error POST /carriersettings:', error);
      throw error;
    }
  }

  async update(id: number, request: SaveCarrierSettingRequest): Promise<void> {
    this.errorSignal.set(null);
    try {
      await firstValueFrom(
        this.httpClient.put<void>(`${this.config.getApiUrl()}/carriersettings/${id}`, request)
      );
    } catch (error) {
      this.errorSignal.set('Error updating carrier setting');
      console.error(`Error PUT /carriersettings/${id}:`, error);
      throw error;
    }
  }

  async delete(id: number): Promise<void> {
    this.errorSignal.set(null);
    try {
      await firstValueFrom(
        this.httpClient.delete<void>(`${this.config.getApiUrl()}/carriersettings/${id}`)
      );
    } catch (error) {
      this.errorSignal.set('Error deleting carrier setting');
      console.error(`Error DELETE /carriersettings/${id}:`, error);
      throw error;
    }
  }

  reset(): void {
    this.settingsSignal.set([]);
    this.settingTypesSignal.set([]);
    this.settingKeysSignal.set([]);
    this.errorSignal.set(null);
  }
}
