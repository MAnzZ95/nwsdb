import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsAllAddComponent } from './lands-all-add.component';

describe('LandsAllAddComponent', () => {
  let component: LandsAllAddComponent;
  let fixture: ComponentFixture<LandsAllAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsAllAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsAllAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
