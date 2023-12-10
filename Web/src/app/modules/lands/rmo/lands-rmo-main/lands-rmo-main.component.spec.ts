import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsRmoMainComponent } from './lands-rmo-main.component';

describe('LandsRmoMainComponent', () => {
  let component: LandsRmoMainComponent;
  let fixture: ComponentFixture<LandsRmoMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsRmoMainComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsRmoMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
