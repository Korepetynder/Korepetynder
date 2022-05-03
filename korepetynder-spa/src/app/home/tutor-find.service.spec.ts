import { TestBed } from '@angular/core/testing';

import { TutorFindService } from './tutor-find.service';

describe('TutorFindService', () => {
  let service: TutorFindService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TutorFindService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
