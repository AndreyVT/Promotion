import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { UserInfo } from '../auth/userInfo';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})

export class LoginFormComponent implements OnInit {

  userInfo: UserInfo;

  googleLoginActive = true;
  isRegisterEnabled = true;

  constructor(private authService: AuthService) {
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
   this.authService.login(this.userInfo);
  }

  ngOnInit() {
  }

}
