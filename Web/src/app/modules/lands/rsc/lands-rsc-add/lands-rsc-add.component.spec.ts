import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsRscAddComponent } from './lands-rsc-add.component';

describe('LandsRscAddComponent', () => {
  let component: LandsRscAddComponent;
  let fixture: ComponentFixture<LandsRscAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsRscAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsRscAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
