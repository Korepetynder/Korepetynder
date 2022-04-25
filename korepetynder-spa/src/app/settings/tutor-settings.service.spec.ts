import { TestBed } from '@angular/core/testing';

import { TutorSettingsService } from './tutor-settings.service';

describe('TutorSettingsService', () => {
  let service: TutorSettingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TutorSettingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
