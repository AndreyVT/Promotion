import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../auth/auth.service';

@Injectable()
export class PermissionsService {

  private apiHostValue = environment.api.host + 'values';
  private adminRole = 1;
  private managerRole = 2;
  private userRole = 3;

  private deny = 0;
  private read = 1;
  private write = 2;

  constructor(private router: Router,  private http: HttpClient, private authService: AuthService) {
  }

  getUserRole() {
    if (this.authService.isLoggedIn) {
      const userSettings = JSON.parse(localStorage.getItem('userSettings'));

      return userSettings.role;
    }
  }

  isSegmentAllow(segmentName: string) {
    if (!this.authService.isLoggedIn) {
      return false;
    }
    const userSettings = JSON.parse(localStorage.getItem('userSettings'));

    let result = false;

    if (userSettings && userSettings.segments) {
      const that = this;
      userSettings.segments.find(function (item) {
        if (item.item1 === segmentName) {
          result = (item.item2 === that.read) || (item.item2 === that.write);
          return;
        }
      });
    }

    return result;
  }
}
