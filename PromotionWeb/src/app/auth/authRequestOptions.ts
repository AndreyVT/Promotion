import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

const AUTH_HEADER_KEY = 'Authorization';
const AUTH_PREFIX = 'Bearer';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  private headers: HttpHeaders = new HttpHeaders();

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authService.isLoggedIn) {
      const token = this.authService.getAccessToken();
      if (token) {
        this.setHeaders();
        const newRq = req.clone({headers: this.headers});

        return next.handle(newRq);
      }
    } else {
      return next.handle(req);
    }
  }

  constructor(private authService: AuthService) {
  }

  private setHeaders() {
    this.headers = new HttpHeaders();
    this.headers = this.headers.set('Content-Type', 'application/json');
    this.headers = this.headers.set('Accept', 'application/json');
    this.headers = this.headers.set('Access-Control-Allow-Origin', '*');

    const token = this.authService.accessToken;
    if (token !== '') {
      const tokenValue = `${AUTH_PREFIX} ${token}`;
      this.headers = this.headers.append(AUTH_HEADER_KEY, tokenValue);
    }
  }
}
