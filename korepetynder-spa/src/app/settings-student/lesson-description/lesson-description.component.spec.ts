import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LessonDescriptionComponent } from './lesson-description.component';

describe('LessonDescriptionComponent', () => {
  let component: LessonDescriptionComponent;
  let fixture: ComponentFixture<LessonDescriptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LessonDescriptionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LessonDescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
