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

  @Output() statusChange = new EventEmitter<boolean>();

  isSaving = false;
  isTutorOldValue = false;

  profileForm = this.fb.group({
    isTutor: [false],
    locations: [[]],
    lessons: this.fb.array([])
  });

  constructor(
    public router: Router,
    private fb: FormBuilder,
    private tutorSettingsService: TutorSettingsService,
    private userService: UserService,
    private _snackBar: MatSnackBar) { }

  get locationsCtrl() {
    return this.profileForm.get('locations') as FormControl;
  }

  get lessons() {
    return this.profileForm.get('lessons') as FormArray;
  }
  get lessonsControls() {
    return this.lessons.controls as FormGroup[];
  }

  get isTutorCtrl() {
    return this.profileForm.get('isTutor') as FormControl;
  }
  get isTutor() {
    return this.isTutorCtrl.value as boolean;
  }

  addLesson(): void {
    this.lessons.push(this.fb.group({
      id: [null],
      subject: [null, [Validators.required]],
      levels: [[]],
      languages: [[]],
      cost: [null],
      frequency: [null],
    }));
  }

  removeLesson(id: number): void {
    this.lessons.removeAt(id);
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
        });
      }
    });

    this.profileForm.statusChanges.subscribe(status => this.statusChange.emit(status === 'VALID'));
  }

  saveChanges(): void {
    if (this.profileForm.invalid) {
      return;
    }

    this.isSaving = true;
    const tutorRequest = new TutorRequest(this.locationsCtrl.value);

    const saveObservable = this.isTutor
      ? (this.isTutorOldValue ? this.tutorSettingsService.updateTutor(tutorRequest) : this.tutorSettingsService.createTutor(tutorRequest))
      : (this.isTutorOldValue ? this.tutorSettingsService.deleteTutor() : of(null));

    saveObservable.subscribe(() => {
      this.isSaving = false;
      this._snackBar.open("Zapisano pomyślnie.", "OK", {duration: 5000});
    });
  }
}
