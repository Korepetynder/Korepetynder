import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { of } from 'rxjs';
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

  get url() { return this.photo.get('url')!.value; }
  set url(url: string | null) { this.photo.get('url')!.setValue(url); }

  isSaving = false;

  progress = 0;
  selectedPhoto: File | null = null;

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

  fileChange(event: Event) {
    const files = (event.target as HTMLInputElement).files;
    if (files === null || files.length === 0) {
      return;
    }

    const fileToUpload = files[0];
    const reader = new FileReader();
    reader.onload = (e: Event) => {
      this.url = (e.target as FileReader).result as string;
    };
    reader.readAsDataURL(fileToUpload);

    this.selectedPhoto = fileToUpload;
  }

  saveChanges(): void {
    if (this.photo.invalid || this.selectedPhoto === null) {
      return;
    }

    this.isSaving = true;
    const formValue = this.photo.value;

    console.log(formValue);

    this.tutorSettingsService
      .uploadPhoto(this.selectedPhoto, this.photo.get('lessons')!.value)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total!);
        } else if (event instanceof HttpResponse) {
          this.isSaving = false;
          if (event.body) {
            this.snackBar.open('Zapisano pomyślnie.', 'OK', { duration: 5000 });
            this.photoId = event.body?.id;
          } else {
            this.snackBar.open('Wystąpił nieoczekiwany błąd.', 'OK', { duration: 5000 });
          }
        }
      });

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
      this.snackBar.open("Usunięto pomyślnie.", "OK", { duration: 5000 });
      this.photoRemove.emit();
    });
  }
}
