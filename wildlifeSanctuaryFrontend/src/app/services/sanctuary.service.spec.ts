import { TestBed } from '@angular/core/testing';

import { SanctuaryService } from './sanctuary.service';

describe('SanctuaryService', () => {
  let service: SanctuaryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SanctuaryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
