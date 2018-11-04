import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

const AUTH_HEADER_KEY = 'Authorization';
const AUTH_PREFIX = 'Bearer';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authService.isLoggedIn) {
      const token = this.authService.getAccessToken();
      if (token) {
        this.setHeaders();
        const newRq = req.clone({headers: this.headers});
        // newRq = newRq.clone({ setHeaders: { 'Authorization': 'Bearer ' + token }});
        // newRq = newRq.clone({ setHeaders: { 'Access-Control-Allow-Origin': '*' }});
        // const hdrs = new Headers().append(AUTH_HEADER_KEY, `${AUTH_PREFIX} ${token}`);
        // const newRq = req.clone({headers: new Headers().append(AUTH_HEADER_KEY, `${AUTH_PREFIX} ${token}`)});
        // console.log('HEADER:: ', newRq);
        return next.handle(newRq);
        // req.headers.
        // req.headers.append(AUTH_HEADER_KEY, `${AUTH_PREFIX} ${token}`);
        // req.clone();
        // req = req.clone(Headers.append(AUTH_HEADER_KEY, `${AUTH_PREFIX} ${token}`));
      }
    } else {
      return next.handle(req);
    }

    /*req = req.clone({
      setHeaders: {
        'Access-Control-Allow-Origin': '*'
      }
    });*/
  }

  constructor(private authService: AuthService) {
  }

  private headers: HttpHeaders = new HttpHeaders();

  private setHeaders() {
    this.headers = new HttpHeaders();
    this.headers = this.headers.set('Content-Type', 'application/json');
    this.headers = this.headers.set('Accept', 'application/json');
    this.headers = this.headers.set('Access-Control-Allow-Origin', '*');

    const token = this.authService.accessToken;
    if (token !== '') {
      const tokenValue = 'Bearer ' + token;
      this.headers = this.headers.append('Authorization', tokenValue);
    }
  }
}
