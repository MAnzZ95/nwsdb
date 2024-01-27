import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Rsc } from 'src/app/core/models/rscs/rsc';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss']
})
export class RscViewComponent {
  inputForm!: FormGroup;
  activeStatus = true;
  data!: Rsc;
  
  constructor(
    private dialog: MatDialog,
    private inputFormBuilder: FormBuilder,
    private router: Router,
  ){}

  onActiveStatusChange(value: boolean) {
    this.activeStatus = value;
  }

  onCancel(): void {
    this.router.navigate([`/land`], {});
  }

  getStatusClass(status: number): string {
    if (status == 0) {
      return 'status-available';
    } else if (status == 1) {
      return 'status-not-available';
    } else if (status == 2) {
      return 'status-delete';
    } else {
      return '';
    }
  }
}
