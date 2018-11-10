import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { UserInfo } from '../auth/userInfo';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';
import { UserSettingsService } from '../users/settings/user-settings.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})

export class LoginFormComponent implements OnInit {

  userInfo: UserInfo;

  googleLoginActive = true;
  isRegisterEnabled = true;

  constructor(private authService: AuthService, private router: Router, private userSettingsService: UserSettingsService) {
    this.userInfo = new UserInfo();
  }

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
   this.authService.login(this.userInfo)
     .pipe(catchError(this.handleError))
     .subscribe(
     (data: {}) => {
       this.userInfo.authInfo = data;
       localStorage.setItem('userInfo', JSON.stringify(this.userInfo));
       this.authService.setAccessToken();

       this.userSettingsService.getUserSettings().subscribe((data1: {}) => {
         console.log(data1);
         this.router.navigate(['/info', {}]);
       },
         error1 => console.log(error1)
       );
     },
     error => console.log(error)
   );
  }

  ngOnInit() {
  }

  // Implement a method to handle errors if any
  private handleError(err: HttpErrorResponse | any) {
    console.error('An error occurred', err);
    return throwError(err.message || err);
  }
}
