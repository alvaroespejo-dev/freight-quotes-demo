import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { ConfigService } from '../../services/config.service';
import {
  AccessorialResponse,
  CarrierResponse,
  ConstantResponse,
  CountryResponse,
  RateQuoteRequest,
  RateQuoteResponse,
  StateResponse
} from './quotes.types';

@Injectable({
  providedIn: 'root'
})
export class QuotesService {

  private httpClient = inject(HttpClient);
  private config = inject(ConfigService);

  // Lookups
  private constantsSignal = signal<ConstantResponse[]>([]);
  private accessorialsSignal = signal<AccessorialResponse[]>([]);
  private countriesSignal = signal<CountryResponse[]>([]);
  private statesSignal = signal<StateResponse[]>([]);
  private carriersSignal = signal<CarrierResponse[]>([]);

  constants = this.constantsSignal.asReadonly();
  accessorials = this.accessorialsSignal.asReadonly();
  countries = this.countriesSignal.asReadonly();
  states = this.statesSignal.asReadonly();
  carriers = this.carriersSignal.asReadonly();

  // Quote results (streamed via SignalR)
  private quotesSignal = signal<RateQuoteResponse[]>([]);
  private quoteErrorsSignal = signal<{ carrier: string; error: string }[]>([]);
  private fetchingSignal = signal(false);
  private errorSignal = signal<string | null>(null);

  quotes = this.quotesSignal.asReadonly();
  quoteErrors = this.quoteErrorsSignal.asReadonly();
  isFetching = this.fetchingSignal.asReadonly();
  error = this.errorSignal.asReadonly();

  private hubConnection?: signalR.HubConnection;

  /** Loads every lookup the form needs in parallel. */
  async loadLookups(): Promise<void> {
    this.errorSignal.set(null);
    try {
      const api = this.config.getApiUrl();
      const constantTypeIds = [1, 2, 3, 6, 9, 10]; // ShippingUnits, SubClass, FreightClass, EquipmentType, Terms, Role
      let params = new HttpParams();
      constantTypeIds.forEach(id => (params = params.append('constantTypeIds', id)));

      const [constants, accessorials, countries] = await Promise.all([
        firstValueFrom(this.httpClient.get<{ constants: ConstantResponse[] }>(`${api}/constants`, { params })),
        firstValueFrom(this.httpClient.get<{ accessorials: AccessorialResponse[] }>(`${api}/accessorials`)),
        firstValueFrom(this.httpClient.get<{ countries: CountryResponse[] }>(`${api}/countries`))
      ]);

      this.constantsSignal.set(constants.constants ?? []);
      this.accessorialsSignal.set(accessorials.accessorials ?? []);
      this.countriesSignal.set(countries.countries ?? []);
    } catch (error) {
      this.errorSignal.set('Error loading form data');
      console.error('Error loading lookups:', error);
    }
  }

  /** Loads every state once; callers filter by country client-side. */
  async loadStates(): Promise<void> {
    try {
      const api = this.config.getApiUrl();
      const response = await firstValueFrom(
        this.httpClient.get<{ states: StateResponse[] }>(`${api}/states`)
      );
      this.statesSignal.set(response.states ?? []);
    } catch (error) {
      console.error('Error loading states:', error);
      this.statesSignal.set([]);
    }
  }

  /**
   * Submits a rate request. Opens the SignalR connection, joins the request
   * group, then POSTs so streamed quotes are not missed.
   */
  async requestQuotes(request: RateQuoteRequest): Promise<void> {
    this.quotesSignal.set([]);
    this.quoteErrorsSignal.set([]);
    this.errorSignal.set(null);
    this.fetchingSignal.set(true);

    try {
      await this.connectHub(request.requestId);
      await firstValueFrom(
        this.httpClient.post(`${this.config.getApiUrl()}/ratequote`, request)
      );
    } catch (error) {
      this.errorSignal.set('Error requesting quotes');
      this.fetchingSignal.set(false);
      console.error('Error POST /ratequote:', error);
      await this.disconnectHub();
    }
  }

  private async connectHub(requestId: string): Promise<void> {
    await this.disconnectHub();

    // apiUrl ends with /api; the hub is mapped at the host root.
    const hubUrl = this.config.getApiUrl().replace(/\/api\/?$/, '') + '/hubs/rate-quote';

    this.hubConnection = new signalR.HubConnectionBuilder()
      // The API's CORS policy replies with a wildcard origin, which the browser
      // rejects on credentialed requests; the hub needs no cookies anyway.
      .withUrl(hubUrl, { withCredentials: false })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on('ReceiveQuotes', (quotes: RateQuoteResponse | RateQuoteResponse[]) => {
      const incoming = Array.isArray(quotes) ? quotes : [quotes];
      this.quotesSignal.update(current => [...current, ...incoming]);
    });

    this.hubConnection.on('ReceiveQuoteError', (payload: { carrier: any; error: string }) => {
      const carrierName = payload?.carrier?.name ?? payload?.carrier?.Name ?? 'Unknown carrier';
      this.quoteErrorsSignal.update(current => [...current, { carrier: carrierName, error: payload?.error }]);
    });

    this.hubConnection.on('AllQuotesCompleted', () => {
      this.fetchingSignal.set(false);
    });

    await this.hubConnection.start();
    await this.hubConnection.invoke('JoinRequest', requestId);
  }

  private async disconnectHub(): Promise<void> {
    if (this.hubConnection) {
      try {
        await this.hubConnection.stop();
      } catch {
        // ignore stop errors
      }
      this.hubConnection = undefined;
    }
  }

  reset(): void {
    this.quotesSignal.set([]);
    this.quoteErrorsSignal.set([]);
    this.errorSignal.set(null);
    this.fetchingSignal.set(false);
    void this.disconnectHub();
  }
}
