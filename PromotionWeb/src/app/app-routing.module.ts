import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { InfoComponent } from './info/info.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { AuthGuard } from './auth/auth.guard';
import { PromoteComponent } from './promote/promote.component';
import { CallbackComponent } from './callback/callback.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { path: '', redirectTo: '/info', pathMatch: 'full' },
  { path: 'info', component: InfoComponent },
  { path: 'login', component: LoginFormComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'callback', component: CallbackComponent },
  { path: 'promote', component: PromoteComponent, canActivate: [AuthGuard], },
]; // loadChildren: './info/info.component'

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
