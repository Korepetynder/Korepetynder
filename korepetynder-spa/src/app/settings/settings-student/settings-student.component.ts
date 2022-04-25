import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
  @Input() isStudent: boolean = false;

  @Input() languages: Language[] = [];
  @Input() levels: Level[] = [];
  @Input() locations: Location[] = [];
  @Input() subjects: Subject[] = [];

  @Output() completed = new EventEmitter<void>();

  isSaving = false;

  profileForm = this.fb.group({
    locations: [[]],
    lessons: this.fb.array([])
  });

  constructor(public router: Router, private fb: FormBuilder, private studentSettingsService: StudentSettingsService) { }

  get locationsCtrl() { return this.profileForm.get('locations') as FormControl; }
  get lessons() {
    return this.profileForm.get('lessons') as FormArray;
  }

  get lessonsControls() {
    return this.lessons.controls as FormGroup[];
  }

  addLesson(): void {
    this.lessons.push(this.fb.group({
      lessonId: [null],
      subject: ['', [Validators.required]],
      levels: [[]],
      languages: [[]],
      minCost: [''],
      maxCost: [''],
      frequency: [''],
    }));
  }

  removeLesson(id: number): void {
    this.lessons.removeAt(id - 1);
  }

  ngOnInit(): void {
    if (this.isStudent) {
      this.studentSettingsService.getStudent().subscribe(student => this.profileForm.patchValue(student));

      this.studentSettingsService.getLessons().subscribe(lessons => {

      });
    }
  }

  saveChanges(): void {
    if (this.profileForm.invalid) {
      return;
    }

    this.isSaving = true;
    const studentRequest = new StudentRequest(this.locationsCtrl.value);

    const saveObservable = this.isStudent
      ? this.studentSettingsService.updateStudent(studentRequest)
      : this.studentSettingsService.createStudent(studentRequest);

    saveObservable.subscribe(() => {
      this.isSaving = false;
      this.completed.emit();
    });
  }
}
