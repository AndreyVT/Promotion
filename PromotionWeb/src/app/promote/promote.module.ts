import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PromoteRoutingModule } from './promote-routing.module';
import { PromoteComponent } from './promote.component';
import { MatCardModule } from '@angular/material';

@NgModule({
  imports: [
    CommonModule,
    PromoteRoutingModule,
    MatCardModule
  ],
  declarations: [PromoteComponent]
})
export class PromoteModule { }
