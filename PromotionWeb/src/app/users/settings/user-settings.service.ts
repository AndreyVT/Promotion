import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../auth/auth.service';
import { UserInfo } from '../../auth/userInfo';
import { environment } from './../../../environments/environment';

@Injectable()
export class UserSettingsService {

  private userInfo: UserInfo;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getUserSettings() {
    if (!this.authService.isLoggedIn) {
      return;
    }

    this.userInfo = this.authService.userProfile;

    return this.http.get(environment.api.host + `users/${this.userInfo.login}/settings`);
  }
}
