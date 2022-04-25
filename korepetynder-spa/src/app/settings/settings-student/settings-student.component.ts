import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { of, take } from 'rxjs';
import { UserService } from 'src/app/shared/services/user.service';
import { StudentRequest } from '../models/requests/studentRequest';
import { Language } from '../models/responses/language';
import { Level } from '../models/responses/level';
import { Location } from '../models/responses/location';
import { Subject } from '../models/responses/subject';
import { StudentSettingsService } from '../student-settings.service';

@Component({
  selector: 'app-settings-student',
  templateUrl: './settings-student.component.html',
  styleUrls: ['./settings-student.component.scss']
})
export class SettingsStudentComponent implements OnInit {
  @Input() isEdit: boolean = false;

  @Input() languages: Language[] = [];
  @Input() levels: Level[] = [];
  @Input() locations: Location[] = [];
  @Input() subjects: Subject[] = [];

  @Output() statusChange = new EventEmitter<boolean>();

  isSaving = false;
  isStudentOldValue = false;
  isSaved = false;

  profileForm = this.fb.group({
    isStudent: [false],
    locations: [[]],
    lessons: this.fb.array([])
  });

  constructor(
    public router: Router,
    private fb: FormBuilder,
    private studentSettingsService: StudentSettingsService,
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

  get isStudentCtrl() {
    return this.profileForm.get('isStudent') as FormControl;
  }
  get isStudent() {
    return this.isStudentCtrl.value as boolean;
  }

  addLesson(): void {
    this.lessons.push(this.fb.group({
      id: [null],
      subject: [null, [Validators.required]],
      levels: [[], [Validators.required]],
      languages: [[], [Validators.required]],
      minCost: [null, [Validators.required]],
      maxCost: [null, [Validators.required]],
      frequency: [null],
    }));
  }

  removeLesson(id: number): void {
    this.lessons.removeAt(id);
  }

  ngOnInit(): void {
    this.userService.isStudent().subscribe(isStudent => {
      this.isStudentOldValue = isStudent;
      this.isStudentCtrl.setValue(isStudent);
      if (isStudent) {
        this.studentSettingsService.getStudent().subscribe(student => this.profileForm.patchValue(student));

        this.studentSettingsService.getLessons().subscribe(lessons => {
          console.log(lessons);
          lessons.forEach(() => this.addLesson());
          this.lessons.patchValue(lessons.map(lesson => ({
            id: lesson.id,
            subject: lesson.subject.id,
            levels: lesson.levels.map(level => level.id),
            languages: lesson.languages.map(language => language.id),
            minCost: lesson.preferredCostMinimum,
            maxCost: lesson.preferredCostMaximum,
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
    this.isSaved = true;
    const studentRequest = new StudentRequest(this.locationsCtrl.value);

    const saveObservable = this.isStudent
      ? (this.isStudentOldValue ? this.studentSettingsService.updateStudent(studentRequest) : this.studentSettingsService.createStudent(studentRequest))
      : (this.isStudentOldValue ? this.studentSettingsService.deleteStudent() : of(null));

    saveObservable.subscribe(() => {
      this.isSaving = false;
      this.snackBar.open("Zapisano pomyślnie.", "OK", {duration: 5000});
    });
  }
}
