import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth/auth.service';
import { PermissionsService } from './shared/services/permissions.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Спасибки';

  constructor(private router: Router, private authService: AuthService, private permissionsService: PermissionsService) {
  }

  promote() {
    this.router.navigate(['/promote', {}]);
  }

  users() {
    this.router.navigate(['/users', {}]);
  }

  management() {
    this.router.navigate(['/management', {}]);
  }
}

