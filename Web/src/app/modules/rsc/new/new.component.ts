import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { Rsc } from 'src/app/core/models/rscs/rsc';
import { RscStatus } from 'src/app/core/models/rscs/rsc-status';
import { RscService } from 'src/app/core/services/rsc.service';
import { SnackBarNotificationService } from 'src/app/core/services/snack-notification.service';
import { DialogComponent } from 'src/app/shared/components';
const EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class RscNewComponent {
  inputForm!: FormGroup;
  activeStatus = true;
  isActiveChecked = true;
  subscriber!: Subscription;
  isLegal = true;
  showDialog = true;
  subject = new Subject<boolean>();


  constructor(
    private dialog: MatDialog,
    private inputFormBuilder: FormBuilder,
    private router: Router,
    private rscService: RscService,
    private snackBarNotification: SnackBarNotificationService,
  ) {}

  ngOnInit() {
    this.inputForm = this.inputFormBuilder.group({
      rscName:[null,Validators.required],
      phone:[null,Validators.required],
      rscNumber:[null,Validators.required],
      address:[null,Validators.required]
    });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      panelClass: 'delete-dialog',
      data: {
        title: 'cancel',
        content:'There is unsaved, changes do you need to cancel?',
        icon: 'warning_amber',
        cancel: 'osos-st btn-outline form-control',
        done: 'osos-st btn-save form-control',
        iconClass: 'mr-5 attention-icon',
        iconColor: 'warn',
      },
    });
    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.subject.next(true);
      } else {
        this.subject.next(false);
      }
    });
  }

  private markFormControlsAsTouched() {
    this.inputForm.markAsTouched();
  }

  saveRsc() {
    this.markFormControlsAsTouched();
    if (this.inputForm.valid) {
      console.log(this.inputForm.value)
      const rscData = this.MapToRsc();
      this.subscriber = this.rscService.createRsc(rscData)
        .subscribe({
          next: () => {
            this.snackBarNotification.show(
              'success',
              'rsc Creation completed'
            );
            this.showDialog = false;
            this.router.navigate(['/rsc'], {
              state: {
                success: true,
                successMessage: 'rsc Creation completed'
              },
            });
          },
        });
    }
  }

  public MapToRsc(): Rsc {
    const data = this.inputForm.value;
    const rscObject: Rsc = {
      id: EMPTY_GUID,
      name: data.rscName,
      rscNumber: data.rscNumber,
      mobile: data.phone,
      address: data.address,
      rscStatus: this.activeStatus ? RscStatus.Active : RscStatus.Inactive,
    }

    return rscObject;
  }

  onActiveStatusChange(value: boolean) {
    this.activeStatus = value;
  }

  onCancel(): void {
    this.router.navigate([`/land`], {});
  }
}
