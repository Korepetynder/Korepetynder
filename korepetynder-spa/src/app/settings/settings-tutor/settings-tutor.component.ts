import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { UserService } from 'src/app/shared/services/user.service';
import { TutorRequest } from '../models/requests/tutorRequest';
import { Language } from '../models/responses/language';
import { Level } from '../models/responses/level';
import { Location } from '../models/responses/location';
import { Subject } from '../models/responses/subject';
import { TutorSettingsService } from '../tutor-settings.service';
import { TutorLesson } from '../models/responses/tutorLesson';
import { PhotoComponent } from './photo/photo.component';

@Component({
  selector: 'app-settings-tutor',
  templateUrl: './settings-tutor.component.html',
  styleUrls: ['./settings-tutor.component.scss']
})
export class SettingsTutorComponent implements OnInit {
  @Input() isEdit: boolean = false;

  @Input() languages: Language[] = [];
  @Input() levels: Level[] = [];
  @Input() locations: Location[] = [];
  @Input() subjects: Subject[] = [];

  savedLessons: TutorLesson[] = [];

  @Output() statusChange = new EventEmitter<boolean>();

  isSaving = false;
  isTutorOldValue = false;

  profileForm = this.fb.group({
    isTutor: [false],
    locations: [[], [Validators.required]],
    lessons: this.fb.array([]),
    photos: this.fb.array([]),
  });

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private tutorSettingsService: TutorSettingsService,
    private userService: UserService,
    private snackBar: MatSnackBar) { }

  get locationsCtrl() {
    return this.profileForm.get('locations') as FormControl;
  }

  get lessons() {
    return this.profileForm.get('lessons') as FormArray;
  }
  get lessonsControls() {
    return this.lessons.controls as FormGroup[];
  }

  get photos() {
    return this.profileForm.get('photos') as FormArray;
  }
  get photosControls() {
    return this.photos.controls as FormGroup[];
  }

  get isTutorCtrl() {
    return this.profileForm.get('isTutor') as FormControl;
  }
  get isTutor() {
    return this.isTutorCtrl.value as boolean;
  }

  setAllLocations() {
    let newLocations = this.locationsCtrl.value;
    for (let i = 0; i < this.locations.length; i++) {
      if (this.locations[i].childrenLocations == null) {
        continue;
      }
      if (newLocations.includes(this.locations[i].id)) {
        this.locations[i].childrenLocations.forEach(t => {
          if (!newLocations.includes(t.id)) {
            newLocations.push(t.id);
          }
        });
      }
    }
    this.locationsCtrl.setValue(newLocations);
  }

  checkParentLocation(id: number): boolean {
    return this.locationsCtrl.value.includes(id);
  }

  addLesson(): void {
    this.lessons.push(this.fb.group({
      id: [null],
      subject: [null, [Validators.required]],
      levels: [[], [Validators.required]],
      languages: [[], [Validators.required]],
      cost: [null, [Validators.required]],
      frequency: [null, [Validators.required]],
    }));
  }

  addPhoto(): void { // TODO
    this.photos.push(this.fb.group({
      id: [null],
      url: [{ value: null, disabled: true }],
      lessons: [[], []],
    }));
  }

  removeLesson(id: number): void {
    this.lessons.removeAt(id);
  }

  removePhoto(id: number): void {
    this.photos.removeAt(id);
  }

  updateSavedLessons(): void {
    console.log("UPDATE", this.savedLessons);
    this.tutorSettingsService.getLessons().subscribe(lessons => this.savedLessons = lessons);
    this.savedLessons = this.savedLessons.slice();
  }

  ngOnInit() {
    this.userService.isTutor().subscribe(isTutor => {
      this.isTutorOldValue = isTutor;
      this.isTutorCtrl.setValue(isTutor);
      if (isTutor) {
        this.tutorSettingsService.getTutor().subscribe(tutor => this.profileForm.patchValue(tutor));

        this.tutorSettingsService.getLessons().subscribe(lessons => {
          console.log(lessons);
          lessons.forEach(() => this.addLesson());
          this.lessons.patchValue(lessons.map(lesson => ({
            id: lesson.id,
            subject: lesson.subject.id,
            levels: lesson.levels.map(level => level.id),
            languages: lesson.languages.map(language => language.id),
            cost: lesson.cost,
            frequency: lesson.frequency
          })));
          console.log(this.lessons);

          this.savedLessons = lessons;
        });

        this.tutorSettingsService.getPhotos().subscribe(photos => {
          console.log(photos);
          photos.forEach(() => this.addPhoto());
          this.photos.patchValue(photos);
        })
      }
    });

    this.profileForm.statusChanges.subscribe(status => this.statusChange.emit(status === 'VALID'));
  }

  private addLocationsWithAllChildrenLocationsSelected(locations: number[]) {
    for (let i = 0; i < this.locations.length; i++) {
      if (this.locations[i].childrenLocations == null || this.locations[i].childrenLocations.length == 0) {
        continue;
      }
      if (!locations.includes(this.locations[i].id) && this.locations[i].childrenLocations.every((x) => locations.includes(x.id))) {
        locations.push(this.locations[i].id);
      }
    }
    return locations;
  }

  saveChanges(): void {
    if (this.profileForm.invalid) {
      this.snackBar.open("Wprowadź poprawne dane.", "OK", {duration: 5000});
      return;
    }

    this.isSaving = true;
    let locations: number[] = this.locationsCtrl.value;
    this.addLocationsWithAllChildrenLocationsSelected(locations);

    const tutorRequest = new TutorRequest(locations);

    const saveObservable = this.isTutor
      ? (this.isTutorOldValue ? this.tutorSettingsService.updateTutor(tutorRequest) : this.tutorSettingsService.createTutor(tutorRequest))
      : (this.isTutorOldValue ? this.tutorSettingsService.deleteTutor() : of(null));

    saveObservable.subscribe(() => {
      this.isSaving = false;
      this.snackBar.open("Zapisano pomyślnie.", "OK", {duration: 5000});
    });
  }

  finish(): void {
    this.router.navigate(['settings']);
  }
}
