import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  template: `
    <button
      [attr.type]="buttonType"
      mat-flat-button
      class="{{ Buttoncss }}"
      (click)="onClick()">
      <mat-icon class="{{ Iconcss }}">{{ iconValue }}</mat-icon>
      {{ lableValue }}
    </button>
  `,
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent {
  @Input() lableValue!: string;
  @Input() iconValue!: string;
  @Input() Buttoncss!: string;
  @Input() buttonType!: string;
  @Input() Iconcss!: string;
  @Output() buttonClicked = new EventEmitter<void>();

  onClick() {
    this.buttonClicked.emit();
  }
}
