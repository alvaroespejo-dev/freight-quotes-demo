import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigService } from '../services/config.service';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptor implements HttpInterceptor {
  constructor(private config: ConfigService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const authToken = 'auth-token'; // TODO: Get from storage/service

    const authReq = request.clone({
      url: this.prepareUrl(request.url),
      setHeaders: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    });

    return next.handle(authReq);
  }

  private prepareUrl(url: string): string {
    if (url.startsWith('http')) {
      return url;
    }

    return `${this.config.getApiUrl()}/${url}`;
  }
}
