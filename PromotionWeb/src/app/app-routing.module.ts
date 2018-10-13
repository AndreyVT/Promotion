import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { InfoComponent } from './info/info.component';

const routes: Routes = [
  { path: '', redirectTo: '/info', pathMatch: 'full' },
  { path: 'info', component: InfoComponent }
]; // loadChildren: './info/info.component'

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
