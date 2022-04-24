import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { map, Observable, startWith } from 'rxjs';
import { SettingsTutorComponent } from '../settings-tutor.component';

@Component({
  selector: 'app-lesson-tutor-description',
  templateUrl: './lesson-tutor-description.component.html',
  styleUrls: ['./lesson-tutor-description.component.scss']
})
export class LessonTutorDescriptionComponent implements OnInit {
  @Input() index: number = 0;
  @Input() lesson: FormGroup = this.fb.group({
    course: [''],
    level: [''],
    cost: [''],
    hoursWeekly: [''],
  });

  optionsCourse: string[] = ['Matematyka', 'Język polski', 'Język angielski'];
  optionsLevel: string[] = ['Szkoła podstawowa', 'Szkoła ogólnokształcąca'];

  filteredOptionsCourse!: Observable<string[]>;
  filteredOptionsLevel!: Observable<string[]>;

  constructor(public settingsTutor: SettingsTutorComponent, private fb: FormBuilder) { }

  ngOnInit() {
    this.filteredOptionsCourse = this.lesson.get('course')!.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(this.optionsCourse, value)),
    );

    this.filteredOptionsLevel = this.lesson.get('level')!.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(this.optionsLevel, value)),
    );
  }

  private _filter(options: string[], value: string): string[] {
    const filterValue = value.toLowerCase();

    return options.filter(option => option.toLowerCase().includes(filterValue));
  }
}
