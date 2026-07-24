import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal } from "@angular/core";
import { ConfigService } from "../../services/config.service";
import { firstValueFrom } from "rxjs";
import { CarrierResponse, SaveCarrierRequest } from "./carrier.types";

@Injectable({
  providedIn: 'root'
})
export class CarrierService  {

  private httpClient = inject(HttpClient);
  private config = inject(ConfigService);

  // Signals 
  private carriersSignal = signal<CarrierResponse[]>([]);
  private loadingSignal = signal(false);
  private errorSignal = signal<string | null>(null);

  // Getters 
  carriers = this.carriersSignal.asReadonly();
  isLoading = this.loadingSignal.asReadonly();
  error = this.errorSignal.asReadonly();

async get(): Promise<void> {
  this.loadingSignal.set(true);
  this.errorSignal.set(null);

  try {
    const response = await firstValueFrom(        
      this.httpClient.get<{ carriers: CarrierResponse[] }>(`${this.config.getApiUrl()}/carriers`)
    );
    this.carriersSignal.set(response.carriers || []);
  } catch (error) {
    this.errorSignal.set('Error loading carriers');
    console.error('Error /carriers:', error);
    this.carriersSignal.set([]);
  } finally {
    this.loadingSignal.set(false);
  }
}

async delete(id: number): Promise<void> {
  this.loadingSignal.set(true);
  this.errorSignal.set(null);

  try {
    await firstValueFrom(
      this.httpClient.delete<void>(`${this.config.getApiUrl()}/carriers/${id}`)
    );

    const updated = this.carriersSignal().filter(c => c.id !== id);
    this.carriersSignal.set(updated);

  } catch (error) {
    this.errorSignal.set('Error deleting carrier');
    console.error(`Error /carriers/${id}:`, error);
  } finally {
    this.loadingSignal.set(false);
  }
}

async create(request: SaveCarrierRequest): Promise<number> {
  this.loadingSignal.set(true);
  this.errorSignal.set(null);

  try {
    return await firstValueFrom(
      this.httpClient.post<number>(`${this.config.getApiUrl()}/carriers`, request)
    );
  } catch (error) {
    this.errorSignal.set('Error creating carrier');
    console.error('Error POST /carriers:', error);
    throw error;
  } finally {
    this.loadingSignal.set(false);
  }
}

async update(id: number, request: SaveCarrierRequest): Promise<void> {
  this.loadingSignal.set(true);
  this.errorSignal.set(null);

  try {
    await firstValueFrom(
      this.httpClient.put<void>(`${this.config.getApiUrl()}/carriers/${id}`, request)
    );
  } catch (error) {
    this.errorSignal.set('Error updating carrier');
    console.error(`Error PUT /carriers/${id}:`, error);
    throw error;
  } finally {
    this.loadingSignal.set(false);
  }
}

  reset(): void {
    this.carriersSignal.set([]);
    this.errorSignal.set(null);
  }
}
