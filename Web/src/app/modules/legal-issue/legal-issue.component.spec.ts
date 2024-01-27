import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LegalIssueComponent } from './legal-issue.component';

describe('LegalIssueComponent', () => {
  let component: LegalIssueComponent;
  let fixture: ComponentFixture<LegalIssueComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LegalIssueComponent]
    });
    fixture = TestBed.createComponent(LegalIssueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
