import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WildlifeListComponent } from './wildlife-list.component';

describe('WildlifeListComponent', () => {
  let component: WildlifeListComponent;
  let fixture: ComponentFixture<WildlifeListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WildlifeListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WildlifeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
