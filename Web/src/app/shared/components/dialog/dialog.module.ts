import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/material.module';
import { SharedModule } from '../../shared.module';

@NgModule({
  declarations: [],
  exports: [],
  imports: [CommonModule, MaterialModule, RouterModule, SharedModule],
})
export class DialogComponentModule {}
