import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
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

  get lessonSubject(): string {
    let subjectId = this.lesson.get('subject')!.value;
    for (let subject of this.subjects) {
      if (subject.id == subjectId) {
        return subject.name;
      }
    }
    return "Nowy opis";
  };

  get lessonLevels(): string {
    let levelsIds = this.lesson.get('levels')!.value;
    let levels: string[] = [];

    for (let level of this.levels) {
      for (let id of levelsIds) {
        if (level.id == id) {
          levels.push(level.name);
        }
      }
    }

    let result: string = "";
    for (let i = 0; i < levels.length; i++) {
      result += levels[i];
      if (i != levels.length - 1) {
        result += ", ";
      }
    }
    return result;
  }

  isSaving = false;

  constructor(private studentSettingsService: StudentSettingsService,
    private snackBar: MatSnackBar) { }

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
      this.snackBar.open("Zapisano pomyślnie.", "OK", {duration: 5000});
      this.lessonId = lesson.id;
    });
  }

  removeLesson(): void {
    const deleteObservable = this.lessonId
      ? this.studentSettingsService.deleteLesson(this.lessonId)
      : of(null);

    deleteObservable.subscribe(() => {
      this.snackBar.open("Usunięto pomyślnie.", "OK", {duration: 5000});
      this.lessonRemove.emit();
    });
  }
}
