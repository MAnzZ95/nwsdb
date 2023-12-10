import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsRscViewComponent } from './lands-rsc-view.component';

describe('LandsRscViewComponent', () => {
  let component: LandsRscViewComponent;
  let fixture: ComponentFixture<LandsRscViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsRscViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsRscViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
