import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { PermissionsService } from '../shared/services/permissions.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router,
    private permissionsService: PermissionsService
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (this.authService.isLoggedIn) {
      console.log('ActivatedRouteSnapshot NEXT: ', next);
      console.log('RouterStateSnapshot STATE: ', state);
      const normalRoute = state.url.substring(state.url.lastIndexOf('/') + 1);
      console.log('normalRoute: ', normalRoute);
      return this.permissionsService.isSegmentAllow(normalRoute);
    }

    this.router.navigate(['/']);
    return false;
  }
}
