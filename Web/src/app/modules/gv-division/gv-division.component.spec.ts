import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GvDivisionComponent } from './gv-division.component';

describe('GvDivisionComponent', () => {
  let component: GvDivisionComponent;
  let fixture: ComponentFixture<GvDivisionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GvDivisionComponent]
    });
    fixture = TestBed.createComponent(GvDivisionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
