import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BiologistDashboardComponent } from './biologist-dashboard.component';

describe('BiologistDashboardComponent', () => {
  let component: BiologistDashboardComponent;
  let fixture: ComponentFixture<BiologistDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BiologistDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BiologistDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
