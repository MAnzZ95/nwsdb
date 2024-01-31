import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { UserType } from 'src/app/core/models/user-types/user-type';
import { User } from 'src/app/core/models/users/user';
import { userStatus } from 'src/app/core/models/users/user-status';
import { SnackBarNotificationService } from 'src/app/core/services/snack-notification.service';
import { UserTypeService } from 'src/app/core/services/user-type.service';
import { UserService } from 'src/app/core/services/user.service';
import { DialogComponent } from 'src/app/shared/components';
const EMPTY_GUID = '00000000-0000-0000-0000-000000000000';
@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class UserNewComponent {

  inputForm!: FormGroup;
  activeStatus = true;
  isActiveChecked = true;
  subscriber!: Subscription;
  isLegal = true;
  showDialog = true;
  subject = new Subject<boolean>();
  positions: UserType[] =[];

  constructor(
    private dialog: MatDialog,
    private inputFormBuilder: FormBuilder,
    private router: Router,
    private userTypeService: UserTypeService,
    private snackBarNotification: SnackBarNotificationService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.inputForm = this.inputFormBuilder.group({
      name:[null,Validators.required],
      phone:[null,Validators.required],
      email:[null,Validators.required],
      password:[null,Validators.required],
      gender:[null,Validators.required],
      position:[null,Validators.required],
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

  saveWss() {
    this.markFormControlsAsTouched();
    if (this.inputForm.valid) {
      console.log(this.inputForm.value)
      const rscData = this.MapToUser();
      this.subscriber = this.userService.createUser(rscData)
        .subscribe({
          next: () => {
            this.snackBarNotification.show(
              'success',
              'wss Creation completed'
            );
            this.showDialog = false;
            this.router.navigate(['/rsc'], {
              state: {
                success: true,
                successMessage: 'wss Creation completed'
              },
            });
          },
        });
    }
  }

  public MapToUser(): User {
    const data = this.inputForm.value;
    const rscObject: User = {
      id: EMPTY_GUID,
      name: data.wssName,
      position: data.wssNumber,
      mobile: data.phone,
      email: data.phone,
      address: data.address,
      gender:data.gender,
      password: data.password,
      userStatus: this.activeStatus ? userStatus.Active : userStatus.Inactive,
    }

    return rscObject;
  }

  onActiveStatusChange(value: boolean) {
    this.activeStatus = value;
  }

  onCancel(): void {
    this.router.navigate([`/user`], {});
  }
}
