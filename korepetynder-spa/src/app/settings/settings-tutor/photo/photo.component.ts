import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { of } from 'rxjs';
import { TutorLessonRequest } from '../../models/requests/tutorLessonRequest';
import { Language } from '../../models/responses/language';
import { Level } from '../../models/responses/level';
import { Subject } from '../../models/responses/subject';
import { TutorLesson } from '../../models/responses/tutorLesson';
import { TutorSettingsService } from '../../tutor-settings.service';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.scss']
})
export class PhotoComponent {
  @Input() index: number = 0;
  @Input() photo!: FormGroup;

  @Input() lessons: TutorLesson[] = [];

  @Output() photoRemove = new EventEmitter<void>();

  get photoId(): number | null { return this.photo.get('id')!.value; }
  set photoId(id: number | null) { this.photo.get('id')!.setValue(id); }

  isSaving = false;

  constructor(private tutorSettingsService: TutorSettingsService,
    private snackBar: MatSnackBar) { }

  lessonShortDescription(id: number): string {
    let lesson = this.lessons.find(lesson => lesson.id === id);
    return lesson ?
      lesson.subject.name + ` [${lesson.cost} zł/h]: `
      + lesson.levels.map(level => level.name).join(', ') + '; '
      + lesson.languages.map(language => language.name).join(', ') + '.'
      : '';
  }

  saveChanges(): void {
    if (this.photo.invalid) {
      return;
    }

    this.isSaving = true;
    const formValue = this.photo.value;

    // TODO

    // const lessonRequest = new TutorLessonRequest(formValue.cost,
    //   formValue.frequency, formValue.subject, formValue.levels, formValue.languages);

    // const saveObservable = this.lessonId
    //   ? this.tutorSettingsService.updateLesson(this.lessonId, lessonRequest)
    //   : this.tutorSettingsService.createLesson(lessonRequest);

    // saveObservable.subscribe(lesson => {
    //   this.isSaving = false;
    //   this.snackBar.open("Zapisano pomyślnie.", "OK", {duration: 5000});
    //   this.lessonId = lesson.id;
    // });
  }

  removePhoto(): void {
    const deleteObservable = this.photoId
      ? this.tutorSettingsService.deletePhoto(this.photoId)
      : of(null);

    deleteObservable.subscribe(() => {
      this.snackBar.open("Usunięto pomyślnie.", "OK", {duration: 5000});
      this.photoRemove.emit();
    });
  }
}
