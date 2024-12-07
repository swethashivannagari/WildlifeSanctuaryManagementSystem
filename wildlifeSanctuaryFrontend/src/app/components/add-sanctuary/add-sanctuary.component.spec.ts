import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSanctuaryComponent } from './add-sanctuary.component';

describe('AddSanctuaryComponent', () => {
  let component: AddSanctuaryComponent;
  let fixture: ComponentFixture<AddSanctuaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddSanctuaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddSanctuaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
