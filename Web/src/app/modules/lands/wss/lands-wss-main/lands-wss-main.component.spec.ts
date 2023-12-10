import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsWssMainComponent } from './lands-wss-main.component';

describe('LandsWssMainComponent', () => {
  let component: LandsWssMainComponent;
  let fixture: ComponentFixture<LandsWssMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsWssMainComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsWssMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
