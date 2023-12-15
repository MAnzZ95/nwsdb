import { NgModule } from '@angular/core';

import { MenuItems } from './menu-items/menu-items';
import { AccordionAnchorDirective, AccordionLinkDirective, AccordionDirective } from './accordion';
import { COMPONENTS } from './components';
import { DialogModule } from '@angular/cdk/dialog';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/material.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective,
    COMPONENTS
  ],
  imports:[
    DialogModule,
    CommonModule,
    MaterialModule,
    RouterModule,
    ReactiveFormsModule,
  ],
  exports: [
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective,
    CommonModule,
    MaterialModule,
    COMPONENTS,
   ],
  providers: [ MenuItems ]
})
export class SharedModule { }
