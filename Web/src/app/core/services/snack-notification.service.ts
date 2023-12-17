import { Injectable, inject } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

type NotificationType = 'success' | 'error';

@Injectable({
  providedIn: 'root',
})
export class SnackBarNotificationService {
  private snackBar = inject(MatSnackBar);

  /**
   * Shows a notification with the given message and type.
   * @param type {NotificationType} type of notification to show. Can be 'success' or 'error'.
   * @param message {string} message to show in the notification.
   * @param config {Partial<MatSnackBarConfig>} optional configuration for the notification.
   *
   * @example
   * this.snackBarNotification.show('success', 'This is a success message');
   * this.snackBarNotification.show('error', 'This is an error message');
   */
  show(
    type: NotificationType,
    message: string,
    config?: Partial<MatSnackBarConfig>
  ): void {
    const defaultConfig: MatSnackBarConfig = {
      duration: 3000,
      panelClass: `mat-mdc-snack-bar-container-${type}`,
      horizontalPosition: 'right',
      verticalPosition: 'bottom',
      politeness: 'assertive',
    };
    this.snackBar.open(message, '', {
      ...defaultConfig,
      ...config,
    });
  }
}
