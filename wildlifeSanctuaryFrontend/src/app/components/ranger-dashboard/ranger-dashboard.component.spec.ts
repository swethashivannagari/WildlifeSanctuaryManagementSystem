import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RangerDashboardComponent } from './ranger-dashboard.component';

describe('RangerDashboardComponent', () => {
  let component: RangerDashboardComponent;
  let fixture: ComponentFixture<RangerDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RangerDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RangerDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
