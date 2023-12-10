import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsRscMainComponent } from './lands-rsc-main.component';

describe('LandsRscMainComponent', () => {
  let component: LandsRscMainComponent;
  let fixture: ComponentFixture<LandsRscMainComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsRscMainComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsRscMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
