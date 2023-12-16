import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DsDivisionComponent } from './ds-division.component';

describe('DsDivisionComponent', () => {
  let component: DsDivisionComponent;
  let fixture: ComponentFixture<DsDivisionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DsDivisionComponent]
    });
    fixture = TestBed.createComponent(DsDivisionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
