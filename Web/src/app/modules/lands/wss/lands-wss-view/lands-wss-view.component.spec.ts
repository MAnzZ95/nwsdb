import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsWssViewComponent } from './lands-wss-view.component';

describe('LandsWssViewComponent', () => {
  let component: LandsWssViewComponent;
  let fixture: ComponentFixture<LandsWssViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsWssViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsWssViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
