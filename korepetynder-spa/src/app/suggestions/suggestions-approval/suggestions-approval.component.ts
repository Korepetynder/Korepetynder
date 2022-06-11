import { Component, OnInit } from '@angular/core';
import { Location } from '../models/responses/location';
import { Level } from '../models/responses/level';
import { Language } from '../models/responses/language';
import { Subject } from '../models/responses/subject';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DictionariesService } from '../../settings/dictionaries.service';
import { FormBuilder, FormControl } from '@angular/forms';
import { SuggestionsApprovalService } from './suggestions-approval.service';

@Component({
  selector: 'app-suggestions-approval',
  templateUrl: './suggestions-approval.component.html',
  styleUrls: ['./suggestions-approval.component.scss']
})
export class SuggestionsApprovalComponent implements OnInit {
  isSaving = false;

  languages: Language[] = [];
  levels: Level[] = [];
  locations: Location[] = [];
  subjects: Subject[] = [];

  subjectForm = this.fb.group({
    subjects: [[]]
  });

  levelForm = this.fb.group({
    levels: [[]]
  });

  languageForm = this.fb.group({
    languages: [[]]
  });

  constructor(
    private dictionariesService: DictionariesService,
    private suggestionsApprovalService: SuggestionsApprovalService,
    private snackBar: MatSnackBar,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.dictionariesService.getLanguagesToApprove().subscribe(languages => this.languages = languages);
    this.dictionariesService.getLevelsToApprove().subscribe(levels => this.levels = levels);
    this.dictionariesService.getLocationsToApprove().subscribe(locations => this.locations = locations);
    this.dictionariesService.getSubjectsToApprove().subscribe(subjects => this.subjects = subjects);
  }

  get subjectCtrl() {
    return this.subjectForm.get('subjects') as FormControl;
  }

  get levelCtrl() {
    return this.levelForm.get('levels') as FormControl;
  }

  get languageCtrl() {
    return this.languageForm.get('languages') as FormControl;
  }

  Approve() {
    console.log(this.subjectCtrl.value);
    for (let id of this.subjectCtrl.value) {
      this.suggestionsApprovalService.approveSubject(id).subscribe(
        () => {
          this.isSaving = false;
          this.snackBar.open("Zaakceptowano pomyślnie.", "OK", {duration: 5000});
        });
    }
    this.subjects = this.subjects.filter((subject) => !this.subjectCtrl.value.includes(subject.id));

    for (let id of this.levelCtrl.value) {
      this.suggestionsApprovalService.approveLevel(id).subscribe(
        () => {
          this.isSaving = false;
          this.snackBar.open("Zaakceptowano pomyślnie.", "OK", {duration: 5000});
        });
    }
    this.levels = this.levels.filter((level) => !this.levelCtrl.value.includes(level.id));

    for (let id of this.languageCtrl.value) {
      this.suggestionsApprovalService.approveLanguage(id).subscribe(
        () => {
          this.isSaving = false;
          this.snackBar.open("Zaakceptowano pomyślnie.", "OK", {duration: 5000});
        });
    }
    this.languages = this.languages.filter((language) => !this.languageCtrl.value.includes(language.id));
  }

  Remove() {
    console.log(this.subjectCtrl.value);
    for (let id of this.subjectCtrl.value) {
      this.suggestionsApprovalService.removeSubject(id).subscribe(
        () => {
          this.isSaving = false;
          this.snackBar.open("Odrzucono pomyślnie.", "OK", {duration: 5000});
        });
    }
    this.subjects = this.subjects.filter((subject) => !this.subjectCtrl.value.includes(subject.id));

    for (let id of this.levelCtrl.value) {
      this.suggestionsApprovalService.removeLevel(id).subscribe(
        () => {
          this.isSaving = false;
          this.snackBar.open("Odrzucono pomyślnie.", "OK", {duration: 5000});
        });
    }
    this.levels = this.levels.filter((level) => !this.levelCtrl.value.includes(level.id));

    for (let id of this.languageCtrl.value) {
      this.suggestionsApprovalService.removeLanguage(id).subscribe(
        () => {
          this.isSaving = false;
          this.snackBar.open("Odrzucono pomyślnie.", "OK", {duration: 5000});
        });
    }
    this.languages = this.languages.filter((language) => !this.languageCtrl.value.includes(language.id));
  }

}
