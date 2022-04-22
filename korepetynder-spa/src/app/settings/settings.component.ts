import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { map, Observable, startWith } from 'rxjs';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  controlCourse = new FormControl();
  controlLevel = new FormControl();

  optionsCourse: string[] = ['Matematyka', 'Język polski', 'Język angielski'];
  optionsLevel: string[] = ['Szkoła podstawowa', 'Szkoła ogólnokształcąca'];

  filteredOptionsCourse!: Observable<string[]>;
  filteredOptionsLevel!: Observable<string[]>;

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
