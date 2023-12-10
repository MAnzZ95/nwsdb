import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsAllViewComponent } from './lands-all-view.component';

describe('LandsAllViewComponent', () => {
  let component: LandsAllViewComponent;
  let fixture: ComponentFixture<LandsAllViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsAllViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsAllViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
