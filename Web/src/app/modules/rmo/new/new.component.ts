import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { RmoStatus } from 'src/app/core/models/rmos/remo-status';
import { Rmo } from 'src/app/core/models/rmos/rmo';
import { Rsc } from 'src/app/core/models/rscs/rsc';
import { RmoService } from 'src/app/core/services/rmo.service';
import { RscService } from 'src/app/core/services/rsc.service';
import { SnackBarNotificationService } from 'src/app/core/services/snack-notification.service';
const EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class RmoNewComponent {

  inputForm!: FormGroup;
  activeStatus = true;
  isActiveChecked = true;
  subscriber!: Subscription;
  isLegal = true;
  showDialog = true;
  subject = new Subject<boolean>();
  rscs: Rsc[] =[];
  
  constructor(
    private dialog: MatDialog,
    private inputFormBuilder: FormBuilder,
    private router: Router,
    private rmoService: RmoService,
    private snackBarNotification: SnackBarNotificationService,
    private rscService: RscService,
  ) {}

  ngOnInit() {
    this.inputForm = this.inputFormBuilder.group({
      rscName:[null,Validators.required],
      phone:[null,Validators.required],
      rscNumber:[null,Validators.required],
      address:[null,Validators.required]
    });
  }

  public MapToRmo(): Rmo {
    const data = this.inputForm.value;
    const rscObject: Rmo = {
      id: EMPTY_GUID,
      name: data.rscName,
      rmoNumber: data.rscNumber,
      mobile: data.phone,
      address: data.address,
      rmoStatus: this.activeStatus ? RmoStatus.Active : RmoStatus.Inactive,
    }

    return rscObject;
  }

  onActiveStatusChange(value: boolean) {
    this.activeStatus = value;
  }

  onCancel(): void {
    this.router.navigate([`/rmo`], {});
  }

  saveRmo() {
    this.markFormControlsAsTouched();
    if (this.inputForm.valid) {
      console.log(this.inputForm.value)
      const rmoData = this.MapToRmo();
      this.subscriber = this.rmoService.createRmo(rmoData)
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

  private markFormControlsAsTouched() {
    this.inputForm.markAsTouched();
  }

  getRscs(){
    this.subscriber = this.rscService.getRscDetails().subscribe({
      next:data => {
        this.rscs = data;
      }
    })
  }
}
