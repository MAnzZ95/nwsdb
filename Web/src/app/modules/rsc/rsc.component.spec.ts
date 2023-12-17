import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RscComponent } from './rsc.component';

describe('RscComponent', () => {
  let component: RscComponent;
  let fixture: ComponentFixture<RscComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RscComponent]
    });
    fixture = TestBed.createComponent(RscComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
