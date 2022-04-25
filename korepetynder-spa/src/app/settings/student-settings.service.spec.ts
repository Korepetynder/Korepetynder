import { TestBed } from '@angular/core/testing';

import { StudentSettingsService } from './student-settings.service';

describe('StudentSettingsService', () => {
  let service: StudentSettingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentSettingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
