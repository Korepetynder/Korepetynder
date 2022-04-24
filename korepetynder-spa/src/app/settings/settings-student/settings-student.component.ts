import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings-student',
  templateUrl: './settings-student.component.html',
  styleUrls: ['./settings-student.component.scss']
})
export class SettingsStudentComponent implements OnInit {
  isStudent: boolean = true;
  profileForm = this.fb.group({
    isStudent: [true],
    lessons: this.fb.array([])
  });

  constructor(public router: Router, private fb: FormBuilder) { }

  get lessons() {
    return this.profileForm.get('lessons') as FormArray;
  }

  get lessonsControls() {
    return this.lessons.controls as FormGroup[];
  }

  addLesson(): void {
    this.lessons.push(this.fb.group({
      course: ['',  [Validators.required]],
      level: ['',  [Validators.required]],
      minCost: [''],
      maxCost: [''],
      hoursWeekly: [''],
    }));
  }

  removeLesson(id: number): void {
    this.lessons.removeAt(id - 1);
  }

  ngOnInit() {
    this.profileForm.valueChanges.subscribe(val => {
      if (val.isStudent === true)
         this.isStudent = true;
      else
         this.isStudent = false;
    });
  }
}
