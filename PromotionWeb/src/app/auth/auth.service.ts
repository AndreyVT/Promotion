import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import { UserInfo } from './userInfo';
import { Observable, throwError} from 'rxjs';

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

    return this.http
      .post(this.identityServerUrl + 'connect/token', body, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
  }

  setAccessToken() {
    const userInfo = JSON.parse(localStorage.getItem('userInfo'));
    if (userInfo) {
      this.setSession(userInfo.authInfo, userInfo);
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

  getUserInfo() {
    if (this.isLoggedIn) {
      return this.userProfile;
    }
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

  public register(registerData: UserInfo): Observable<any> {
    return this.http.post<UserInfo>(this.identityServerUrl + 'api/user/register', registerData, this.httpOptions);
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
