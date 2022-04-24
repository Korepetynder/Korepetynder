import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonTutorDescriptionComponent } from './lesson-tutor-description.component';

describe('LessonTutorDescriptionComponent', () => {
  let component: LessonTutorDescriptionComponent;
  let fixture: ComponentFixture<LessonTutorDescriptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonTutorDescriptionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonTutorDescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
