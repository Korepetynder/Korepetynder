import { Component, OnInit, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { DictionariesService } from '../dictionaries.service';
import { Language } from '../models/responses/language';
import { Level } from '../models/responses/level';
import { Location } from '../models/responses/location';
import { Subject } from '../models/responses/subject';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  @ViewChild(MatStepper) stepper: MatStepper | undefined;

  languages: Language[] = [];
  levels: Level[] = [];
  locations: Location[] = [];
  subjects: Subject[] = [];

  generalStepCompleted = false;
  studentStepCompleted = false;

  constructor(private dictionariesService: DictionariesService, public router: Router) { }

  ngOnInit(): void {
    this.dictionariesService.getLanguages().subscribe(languages => this.languages = languages);
    this.dictionariesService.getLevels().subscribe(levels => this.levels = levels);
    this.dictionariesService.getLocations().subscribe(locations => this.locations = locations);
    this.dictionariesService.getSubjects().subscribe(subjects => this.subjects = subjects);
  }

  onGeneralStepComplete(): void {
    this.generalStepCompleted = true;
    this.stepper?.next();
  }

  onStudentStepComplete(): void {
    this.studentStepCompleted = true;
    this.stepper?.next();
  }
}
