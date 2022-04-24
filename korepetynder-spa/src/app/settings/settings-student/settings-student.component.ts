import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Lesson } from './lesson-description/lesson-model';

@Component({
  selector: 'app-settings-student',
  templateUrl: './settings-student.component.html',
  styleUrls: ['./settings-student.component.scss']
})
export class SettingsStudentComponent {

  constructor(public router: Router) { }

  lessons: Lesson[] = [];

  addLesson(): void {
    this.lessons.push({} as Lesson);
  }

  removeLesson(id: number): void {
    this.lessons.splice(id - 1, 1);
  }
}
