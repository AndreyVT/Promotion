import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../auth/auth.service';

@Injectable()
export class PermissionsService {

  private apiHostValue = environment.api.host + 'values';

  constructor(private router: Router,  private http: HttpClient, private authService: AuthService) {
  }

  getUserRole() {
    if (this.authService.isLoggedIn) {
      const userSettings = JSON.parse(localStorage.getItem('userSettings'));

      return userSettings;
    }
  }

  isSegmentVisible(segmentName: string) {

  }
}
