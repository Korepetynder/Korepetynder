import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { map, Observable, startWith } from 'rxjs';
import { SettingsStudentComponent } from '../settings-student.component';

@Component({
  selector: 'app-lesson-description',
  templateUrl: './lesson-description.component.html',
  styleUrls: ['./lesson-description.component.scss']
})
export class LessonDescriptionComponent implements OnInit {
  @Input() index: number = 0;
  @Input() lesson: any;

  controlCourse = new FormControl();
  controlLevel = new FormControl();

  optionsCourse: string[] = ['Matematyka', 'Język polski', 'Język angielski'];
  optionsLevel: string[] = ['Szkoła podstawowa', 'Szkoła ogólnokształcąca'];

  filteredOptionsCourse!: Observable<string[]>;
  filteredOptionsLevel!: Observable<string[]>;

  constructor(public settingsStudent: SettingsStudentComponent) { }

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
