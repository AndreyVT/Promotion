import { Component, OnInit } from '@angular/core';
import { UserInfo } from '../auth/userInfo';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  userInfo: UserInfo;

  constructor(private authService: AuthService, private router: Router) {
    this.userInfo = new UserInfo();
  }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.userInfo).subscribe(value => {
        this.router.navigate(['/login', {}]);
      },
      error => {
        // error - объект ошибки
        console.log(error);
      });
  }

}
