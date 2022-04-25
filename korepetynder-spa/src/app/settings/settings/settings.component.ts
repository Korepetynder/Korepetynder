import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
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
  tutorStepCompleted = false;

  selectedTab = 0;

  isEdit = true;

  constructor(
    private dictionariesService: DictionariesService,
    public router: Router,
    private changeDetectionRef: ChangeDetectorRef,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.dictionariesService.getLanguages().subscribe(languages => this.languages = languages);
    this.dictionariesService.getLevels().subscribe(levels => this.levels = levels);
    this.dictionariesService.getLocations().subscribe(locations => this.locations = locations);
    this.dictionariesService.getSubjects().subscribe(subjects => this.subjects = subjects);

    const urlTree = this.router.parseUrl(this.router.url);
    urlTree.queryParams = {};
    const url = urlTree.toString();
    if (url.endsWith('init')) {
      this.isEdit = false;
    }

    this.activatedRoute.queryParams.subscribe(params => {
      if (params['tabId']) {
        this.selectedTab = params['tabId'];
      }
    });

  }

  onGeneralStepComplete(): void {
    this.generalStepCompleted = true;
    this.changeDetectionRef.detectChanges();
    this.stepper?.next();
  }

  onStudentStepStatusChange(status: boolean): void {
    this.studentStepCompleted = status;
  }

  onTutorStepStatusChange(status: boolean): void {
    this.tutorStepCompleted = status;
  }
}
