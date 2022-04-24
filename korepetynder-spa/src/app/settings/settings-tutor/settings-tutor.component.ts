import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Lesson } from './lesson-tutor-description/lesson-model';

@Component({
  selector: 'app-settings-tutor',
  templateUrl: './settings-tutor.component.html',
  styleUrls: ['./settings-tutor.component.scss']
})
export class SettingsTutorComponent implements OnInit {
  isTutor: boolean = true;
  profileForm = this.fb.group({
    isTutor: [true],
  });

  constructor(public router: Router, private fb: FormBuilder) { }

  lessons: Lesson[] = [];

  addLesson(): void {
    this.lessons.push({} as Lesson);
  }

  removeLesson(id: number): void {
    this.lessons.splice(id - 1, 1);
  }

  ngOnInit() {
    this.profileForm.valueChanges.subscribe(val => {
      if (val.isTutor === true)
         this.isTutor = true;
      else
         this.isTutor = false;
    });
  }
}
