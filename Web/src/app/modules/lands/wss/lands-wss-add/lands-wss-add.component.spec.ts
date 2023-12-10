import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsWssAddComponent } from './lands-wss-add.component';

describe('LandsWssAddComponent', () => {
  let component: LandsWssAddComponent;
  let fixture: ComponentFixture<LandsWssAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsWssAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsWssAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
