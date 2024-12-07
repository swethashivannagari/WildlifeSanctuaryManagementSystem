import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConservationistComponent } from './conservationist.component';

describe('ConservationistComponent', () => {
  let component: ConservationistComponent;
  let fixture: ComponentFixture<ConservationistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConservationistComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConservationistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
