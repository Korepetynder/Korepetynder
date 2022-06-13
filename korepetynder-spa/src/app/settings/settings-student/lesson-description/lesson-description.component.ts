import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ThumbnailsMode } from 'ng-gallery';
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
  get minCost() { return this.lesson.get('minCost')!; }
  get maxCost() { return this.lesson.get('maxCost')!; }

  get lessonSubject(): string {
    const subjectId = this.lesson.get('subject')!.value;

    const subject = this.subjects.find(s => s.id == subjectId);

    return subject ? subject.name : "Nowy opis";
  };

  get lessonLevels(): string {
    const levelsIds: number[] = this.lesson.get('levels')!.value;

    const levels = levelsIds.map(id => this.levels.find(level => level.id == id)!.name);

    return levels.join(", ");
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
