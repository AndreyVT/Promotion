import {Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})

export class LoginFormComponent implements OnInit {

  private identityUrl = 'http://localhost:5000/connect/token';
  private receivedData = {};

  private identityServiceClient_id = 'ro.client';
  private identityServiceClient_secret = 'secret';
  private identityServiceGrant_type = 'password';
  private userName = 'alice';
  private password = 'password';

  constructor(private http: HttpClient) { }
/* var data = "client_id=" + UFMSettings.identityServiceClient_id +
                "&client_secret=" + UFMSettings.identityServiceClient_secret +
                "&grant_type=" + UFMSettings.identityServiceGrant_type +
                "&username=" + loginData.userName +
                "&password=" + loginData.password;

            var deferred = $q.defer();
            // --disable-web-security
            $http.post(serviceBase + '/connect/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }
* */
  // Implement a method to get the public deals
  login() {
    const body = `client_id=${this.identityServiceClient_id}&client_secret=${this.identityServiceClient_secret}&` +
    `grant_type=${this.identityServiceGrant_type}&username=${this.userName}&password=${this.password}`;

    this.http
      .post(this.identityUrl, body, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
      .pipe(
        catchError(this.handleError)
      ) .subscribe(
      (data: {}) => {this.receivedData = data; },
      error => console.log(error)
    );
  }

  // Implement a method to handle errors if any
  private handleError(err: HttpErrorResponse | any) {
    console.error('An error occurred', err);
    return throwError(err.message || err);
  }

  ngOnInit() {
  }

}
