import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WssComponent } from './wss.component';

describe('WssComponent', () => {
  let component: WssComponent;
  let fixture: ComponentFixture<WssComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WssComponent]
    });
    fixture = TestBed.createComponent(WssComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
