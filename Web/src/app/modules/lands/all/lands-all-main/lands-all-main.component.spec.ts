import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsAllMainComponent } from './lands-all-main.component';

describe('LandsAllMainComponent', () => {
  let component: LandsAllMainComponent;
  let fixture: ComponentFixture<LandsAllMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsAllMainComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsAllMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
