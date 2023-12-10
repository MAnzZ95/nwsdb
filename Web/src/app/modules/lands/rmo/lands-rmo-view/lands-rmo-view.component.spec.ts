import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsRmoViewComponent } from './lands-rmo-view.component';

describe('LandsRmoViewComponent', () => {
  let component: LandsRmoViewComponent;
  let fixture: ComponentFixture<LandsRmoViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsRmoViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsRmoViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
