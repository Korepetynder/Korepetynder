import { Component, OnInit } from '@angular/core';
import { Lesson } from './lesson-tutor-description/lesson-model';

@Component({
  selector: 'app-settings-tutor',
  templateUrl: './settings-tutor.component.html',
  styleUrls: ['./settings-tutor.component.scss']
})
export class SettingsTutorComponent {
  lessons: Lesson[] = [];

  addLesson(): void {
    this.lessons.push({} as Lesson);
  }

  removeLesson(id: number): void {
    this.lessons.splice(id - 1, 1);
  }

}
