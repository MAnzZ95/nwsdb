import { Component, Inject, OnInit, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-popup-common',
  templateUrl: './delete-popup-common.component.html',
  styleUrls: ['./delete-popup-common.component.scss'],
})
export class DeletePopupCommonComponent implements OnInit {
  deleteMessage!: string;

  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: { message: string; id: string; entity: number },
    private dialogRef: MatDialogRef<DeletePopupCommonComponent>
  ) {}

  ngOnInit() {
    if (this.data.message != null && this.data.message != undefined) {
      this.deleteMessage = this.data.message;
    } else {
      this.deleteMessage = 'configuration.alertMessage.delete.message';
    }
  }

  deleteRow() {
    this.dialogRef.close(true);
  }

  closePopup(): void {
    this.dialogRef.close(false);
  }
}
