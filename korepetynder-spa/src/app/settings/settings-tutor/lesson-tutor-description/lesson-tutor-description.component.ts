import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { of } from 'rxjs';
import { TutorLessonRequest } from '../../models/requests/tutorLessonRequest';
import { Language } from '../../models/responses/language';
import { Level } from '../../models/responses/level';
import { Subject } from '../../models/responses/subject';
import { TutorSettingsService } from '../../tutor-settings.service';

@Component({
  selector: 'app-lesson-tutor-description',
  templateUrl: './lesson-tutor-description.component.html',
  styleUrls: ['./lesson-tutor-description.component.scss']
})
export class LessonTutorDescriptionComponent {
  @Input() index: number = 0;
  @Input() lesson!: FormGroup;

  @Input() languages: Language[] = [];
  @Input() levels: Level[] = [];
  @Input() subjects: Subject[] = [];

  @Output() lessonRemove = new EventEmitter<void>();

  get lessonId(): number | null { return this.lesson.get('id')!.value; }
  set lessonId(id: number | null) { this.lesson.get('id')!.setValue(id); }

  isSaving = false;

  constructor(private tutorSettingsService: TutorSettingsService,
    private _snackBar: MatSnackBar) { }

  saveChanges(): void {
    if (this.lesson.invalid) {
      return;
    }

    this.isSaving = true;
    const formValue = this.lesson.value;
    const lessonRequest = new TutorLessonRequest(formValue.cost,
      formValue.frequency, formValue.subject, formValue.levels, formValue.languages);

    const saveObservable = this.lessonId
      ? this.tutorSettingsService.updateLesson(this.lessonId, lessonRequest)
      : this.tutorSettingsService.createLesson(lessonRequest);

    saveObservable.subscribe(lesson => {
      this.isSaving = false;
      this._snackBar.open("Zapisano pomyślnie.", "OK", {duration: 5000});
      this.lessonId = lesson.id;
    });
  }

  removeLesson(): void {
    const deleteObservable = this.lessonId
      ? this.tutorSettingsService.deleteLesson(this.lessonId)
      : of(null);

    deleteObservable.subscribe(() => {
      this._snackBar.open("Usunięto pomyślnie.", "OK", {duration: 5000});
      this.lessonRemove.emit();
    });
  }
}
