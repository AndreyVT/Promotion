import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { UserInfo } from './userInfo';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

(window as any).global = window;

@Injectable()
export class AuthService {
  // Store authentication data
  expiresAt: number;
  userProfile: any;
  accessToken: string;
  authenticated: boolean;

  private identityServerUrl = environment.auth.identityServerUrl;
  private identityServiceClient_id = environment.auth.identityServiceClient_id;
  private identityServiceClient_secret = environment.auth.identityServiceClient_secret;
  private identityServiceGrant_type = environment.auth.identityServiceGrant_type;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };

  constructor(private router: Router,  private http: HttpClient) {
    this.setAccessToken();
  }

  login(userInfo: UserInfo) {
    const body = `client_id=${this.identityServiceClient_id}&client_secret=${this.identityServiceClient_secret}&` +
      `grant_type=${this.identityServiceGrant_type}&username=${userInfo.login}&password=${userInfo.password}`;

    this.http
      .post(this.identityServerUrl + 'connect/token', body, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
      .pipe(
        catchError(this.handleError)
      ) .subscribe(
      (data: {}) => {
        userInfo.authInfo = data;
        localStorage.setItem('userInfo', JSON.stringify(userInfo));
        this.setSession(data, {});
        console.log(userInfo);
        this.router.navigate(['/info', {}]);
      },
      error => console.log(error)
    );
  }

  setAccessToken() {
    const userInfo = JSON.parse(localStorage.getItem('userInfo'));
    if (userInfo) {
      this.setSession(userInfo.authInfo, {});
    }
  }

  getAccessToken() {
    return this.accessToken;
  }

  private setSession(authResult, profile) {
    // Save authentication data and update login status subject
    this.expiresAt = authResult.expires_in * 100000 + Date.now();
    this.accessToken = authResult.access_token;
    this.userProfile = profile;
    this.authenticated = true;
  }

  getUserInfo(authResult) {
    // Use access token to retrieve user's profile and set session
    /*this.auth0.client.userInfo(authResult.accessToken, (err, profile) => {
      if (profile) {
        this._setSession(authResult, profile);
      }
    });*/
  }

  logout() {
    // Log out of Auth0 session
    // Ensure that returnTo URL is specified in Auth0
    // Application settings for Allowed Logout URLs
    localStorage.clear();
    this.expiresAt = Date.now();
    this.accessToken = null;
    this.userProfile = null;
    this.authenticated = null;

    this.router.navigate(['/info', {}]);
  }

  get isLoggedIn(): boolean {
    // Check if current date is before token
    // expiration and user is signed in locally
    return Date.now() < this.expiresAt && this.authenticated;
  }

  register(registerData: UserInfo) {
    console.log('UserInfo: ', registerData);
    this.http.post<UserInfo>(this.identityServerUrl + 'api/user/register', registerData, this.httpOptions)
      .subscribe(
        (data: any) => {
          console.log(data);
          this.router.navigate(['/login', {}]);
        },
        error => console.log(error)
      );
  }

  // Implement a method to handle errors if any
  private handleError(err: HttpErrorResponse | any) {
    console.error('An error occurred', err);
    return throwError(err.message || err);
  }

  handleLoginCallback() {
    console.log('Callback:', window.location.hash);

    // this.http.get('http://localhost:5000/')
    // When Auth0 hash parsed, get profile
    /*this.auth0.parseHash((err, authResult) => {
      if (authResult && authResult.accessToken) {
        console.log(`Info auth: ${authResult.accessToken}`);
        window.location.hash = '';
        this.getUserInfo(authResult);
      } else if (err) {
        console.error(`Error: ${err.error}`);
      }
      this.router.navigate(['/']);
    });*/
  }
}
