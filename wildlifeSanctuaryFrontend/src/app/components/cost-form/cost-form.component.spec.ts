import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CostFormComponent } from './cost-form.component';

describe('CostFormComponent', () => {
  let component: CostFormComponent;
  let fixture: ComponentFixture<CostFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CostFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CostFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
