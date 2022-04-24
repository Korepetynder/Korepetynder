import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Lesson } from './lesson-tutor-description/lesson-model';

@Component({
  selector: 'app-settings-tutor',
  templateUrl: './settings-tutor.component.html',
  styleUrls: ['./settings-tutor.component.scss']
})
export class SettingsTutorComponent {

  constructor(public router: Router) { }

  lessons: Lesson[] = [];

  addLesson(): void {
    this.lessons.push({} as Lesson);
  }

  removeLesson(id: number): void {
    this.lessons.splice(id - 1, 1);
  }

}
