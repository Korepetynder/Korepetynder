import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Lesson } from './lesson-description/lesson-model';

@Component({
  selector: 'app-settings-student',
  templateUrl: './settings-student.component.html',
  styleUrls: ['./settings-student.component.scss']
})
export class SettingsStudentComponent implements OnInit {
  isStudent: boolean = true;
  profileForm = this.fb.group({
    isStudent: [true],
  });

  constructor(public router: Router, private fb: FormBuilder) { }

  // get lessons() {
  //   return this.profileForm.get('lessons') as FormArray;
  // }

  // addLesson(): void {
  //   this.lessons.push(this.fb.group({
  //     course: [''],
  //     level: [''],
  //     minCost: [''],
  //     maxCost: [''],
  //     hoursWeekly: [''],
  //   }));
  // }

  // removeLesson(id: number): void {
  //   this.lessons.removeAt(id - 1);
  // }

  lessons: Lesson[] = [];

  addLesson(): void {
    this.lessons.push({} as Lesson);
  }

  removeLesson(id: number): void {
    this.lessons.splice(id - 1, 1);
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
