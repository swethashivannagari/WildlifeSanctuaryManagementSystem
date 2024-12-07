import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnvironmentalFormComponent } from './environmental-form.component';

describe('EnvironmentalFormComponent', () => {
  let component: EnvironmentalFormComponent;
  let fixture: ComponentFixture<EnvironmentalFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnvironmentalFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EnvironmentalFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
