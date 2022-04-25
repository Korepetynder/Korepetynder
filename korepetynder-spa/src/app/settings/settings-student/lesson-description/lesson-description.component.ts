import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { of } from 'rxjs';
import { StudentLessonRequest } from '../../models/requests/studentLessonRequest';
import { Language } from '../../models/responses/language';
import { Level } from '../../models/responses/level';
import { Subject } from '../../models/responses/subject';
import { StudentSettingsService } from '../../student-settings.service';

@Component({
  selector: 'app-lesson-description',
  templateUrl: './lesson-description.component.html',
  styleUrls: ['./lesson-description.component.scss']
})
export class LessonDescriptionComponent {
  @Input() index: number = 0;
  @Input() lesson!: FormGroup;

  @Input() languages: Language[] = [];
  @Input() levels: Level[] = [];
  @Input() subjects: Subject[] = [];

  @Output() lessonRemove = new EventEmitter<void>();

  get lessonId(): number | null { return this.lesson.get('id')!.value; }
  set lessonId(id: number | null) { this.lesson.get('id')!.setValue(id); }

  isSaving = false;

  constructor(private studentSettingsService: StudentSettingsService) { }

  saveChanges(): void {
    if (this.lesson.invalid) {
      return;
    }

    this.isSaving = true;
    const formValue = this.lesson.value;
    const lessonRequest = new StudentLessonRequest(formValue.minCost, formValue.maxCost,
      formValue.frequency, formValue.subject, formValue.levels, formValue.languages);

    const saveObservable = this.lessonId
      ? this.studentSettingsService.updateLesson(this.lessonId, lessonRequest)
      : this.studentSettingsService.createLesson(lessonRequest);

    saveObservable.subscribe(lesson => {
      this.isSaving = false;
      this.lessonId = lesson.id;
    });
  }

  removeLesson(): void {
    const deleteObservable = this.lessonId
      ? this.studentSettingsService.deleteLesson(this.lessonId)
      : of(null);

    deleteObservable.subscribe(() => {
      this.lessonRemove.emit();
    });
  }
}
