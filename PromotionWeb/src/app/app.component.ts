import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth/auth.service';
import { ValueService } from './shared/services/value.service';
import { PermissionsService } from './shared/services/permissions.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Спасибки';

  constructor(private router: Router, private authService: AuthService, private valueService: ValueService,
              private permissionsService: PermissionsService) {
  }

  ngOnInit() {
    if (this.authService.isLoggedIn) {
      this.valueService.getValues() // проверка на актуальность аутентификации
        .subscribe(
          (data: {}) => {
          },
          error1 => {
            console.log('Error auth: ', error1);
            if (error1.status === 401) {
              this.authService.logout();
              this.router.navigate(['/info', {}]);
            }
          }
        );
    }
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

