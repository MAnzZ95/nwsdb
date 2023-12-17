import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ThemePalette } from '@angular/material/core';

@Component({
  selector: 'app-switchbase',
  templateUrl: './switchbase.component.html',
  styleUrls: ['./switchbase.component.scss'],
})
export class SwitchbaseComponent implements OnInit {
  color: ThemePalette = 'primary';
  @Input()
  checked = true;
  @Input() disabled = false;
  @Input() switchbasestyle!: string;
  @Input() switchbasecolor!: string;
  @Input() labelValue!: string;
  @Output() valueChange: EventEmitter<boolean> = new EventEmitter<boolean>();
  valueControl!: FormControl;

  onValueChange(event: any) {
    this.valueChange.emit(event.checked);
  }

  ngOnInit(): void {
    this.valueControl = new FormControl(
      { value: this.checked, disabled: this.disabled },
      Validators.required
    );

    if (this.disabled) {
      this.valueControl.disable();
    }
  }
}
