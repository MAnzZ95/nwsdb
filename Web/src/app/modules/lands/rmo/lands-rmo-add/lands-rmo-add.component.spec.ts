import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LandsRmoAddComponent } from './lands-rmo-add.component';

describe('LandsRmoAddComponent', () => {
  let component: LandsRmoAddComponent;
  let fixture: ComponentFixture<LandsRmoAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LandsRmoAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LandsRmoAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
