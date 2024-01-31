import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { Rsc } from 'src/app/core/models/rscs/rsc';
import { RscStatus } from 'src/app/core/models/rscs/rsc-status';
import { RscService } from 'src/app/core/services/rsc.service';
import { SnackBarNotificationService } from 'src/app/core/services/snack-notification.service';
import { DialogComponent } from 'src/app/shared/components';
const EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class RscEditComponent {
  inputForm!: FormGroup;
  activeStatus = true;
  data!: Rsc;
  subject = new Subject<boolean>();
  subscriber!: Subscription;
  showDialog = true;

  constructor(
    private dialog: MatDialog,
    private inputFormBuilder: FormBuilder,
    private router: Router,
    private rscService : RscService,
    private route: ActivatedRoute,
    private snackBarNotification: SnackBarNotificationService,
    private cdr: ChangeDetectorRef,
  ){}

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

  onActiveStatusChange(value: boolean) {
    this.activeStatus = value;
  }

  onCancel(): void {
    this.router.navigate([`/land`], {});
  }

  getRscDetails() {
    const id =this.getRscIdFromRouting() ?? '';
    const sub = this.rscService.getRscDetailsById(id).subscribe({
      next: response => {
        this.data = response;
        if (this.data !== null) {
          this.rscDetailsInForm(this.data);
          this.cdr.detectChanges();
        }
      },
    });
    this.subscriber.add(sub);
  }

  private getRscIdFromRouting(): string {
    const id: string = this.route.snapshot.params['id'];
    return id;
  }

  rscDetailsInForm(rsc: Rsc) {
    this.inputForm.patchValue({
      id: rsc.id,
      name: rsc.name,
      mobile:rsc.mobile,
      rscNumber:rsc.rscNumber,
      address: rsc.address,

    });
  }

  updateRsc() {
    this.markFormControlsAsTouched();
    if (this.inputForm.valid) {
      console.log(this.inputForm.value)
      const rscData = this.MapToRsc();
      this.subscriber = this.rscService.modifyRsc(rscData)
        .subscribe({
          next: () => {
            this.snackBarNotification.show(
              'success',
              'land Creation completed'
            );
            this.showDialog = false;
            this.router.navigate(['/land'], {
              state: {
                success: true,
                successMessage: 'land Creation completed'
              },
            });
          },
        });
    }
  }

  private markFormControlsAsTouched() {
    this.inputForm.markAsTouched();
  }

  public MapToRsc(): Rsc {
    const rscData = this.inputForm.value;
    const id = this.getRscIdFromRouting()??EMPTY_GUID;
    const rscObject: Rsc = {
      id: id ?? EMPTY_GUID,
      name: rscData.rscName,
      mobile: rscData.phone,
      address: rscData.address,
      rscNumber: rscData.rscNumber,
      rscStatus: this.activeStatus ? RscStatus.Active : RscStatus.Inactive,
    }

    return rscObject;
  }
}
