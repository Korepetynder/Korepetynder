import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { map, Observable, startWith } from 'rxjs';
import { SettingsTutorComponent } from '../settings-tutor.component';
import { Lesson } from './lesson-model';

@Component({
  selector: 'app-lesson-tutor-description',
  templateUrl: './lesson-tutor-description.component.html',
  styleUrls: ['./lesson-tutor-description.component.scss']
})
export class LessonTutorDescriptionComponent implements OnInit {
  @Input() index: number = 0;
  @Input() lesson: Lesson = {} as Lesson;

  controlCourse = new FormControl();
  controlLevel = new FormControl();

  optionsCourse: string[] = ['Matematyka', 'Język polski', 'Język angielski'];
  optionsLevel: string[] = ['Szkoła podstawowa', 'Szkoła ogólnokształcąca'];

  filteredOptionsCourse!: Observable<string[]>;
  filteredOptionsLevel!: Observable<string[]>;

  constructor(public settingsTutor: SettingsTutorComponent) { }

  ngOnInit() {
    this.filteredOptionsCourse = this.controlCourse.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(this.optionsCourse, value)),
    );

    this.filteredOptionsLevel = this.controlLevel.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(this.optionsLevel, value)),
    );
  }

  private _filter(options: string[], value: string): string[] {
    const filterValue = value.toLowerCase();

    return options.filter(option => option.toLowerCase().includes(filterValue));
  }
}
