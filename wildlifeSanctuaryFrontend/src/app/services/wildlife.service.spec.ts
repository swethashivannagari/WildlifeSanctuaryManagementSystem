import { TestBed } from '@angular/core/testing';

import { WildlifeService } from './wildlife.service';

describe('WildlifeService', () => {
  let service: WildlifeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WildlifeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
