import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SanctuaryListComponent } from './sanctuary-list.component';

describe('SanctuaryListComponent', () => {
  let component: SanctuaryListComponent;
  let fixture: ComponentFixture<SanctuaryListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SanctuaryListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SanctuaryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
