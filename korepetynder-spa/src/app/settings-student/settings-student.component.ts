import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-settings-student',
  templateUrl: './settings-student.component.html',
  styleUrls: ['./settings-student.component.scss']
})
export class SettingsStudentComponent {
  lessons: number[] = [0, 0];

  addLesson(): void {
    this.lessons.push(0);
  }

  removeLesson(id: number): void {
    this.lessons.splice(id - 1, 1);
  }
}
