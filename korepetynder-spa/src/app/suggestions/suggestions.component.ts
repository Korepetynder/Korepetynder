import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LanguageRequest } from './models/requests/languageRequest';
import { LevelRequest } from './models/requests/levelRequest';
import { SubjectRequest } from './models/requests/subjectRequest';
import { SuggestionsService } from './suggestions.service';
import { DictionariesService } from '../settings/dictionaries.service';
import { Location } from './models/responses/location';
import { LocationRequest } from './models/requests/locationRequest';

@Component({
  selector: 'app-suggestions',
  templateUrl: './suggestions.component.html',
  styleUrls: ['./suggestions.component.scss']
})
export class SuggestionsComponent implements OnInit {
  locations: Location[] = [];
  // locations: Location[] = [{
  //   id: 1,
  //   name: "lol",
  //   parentId: null,
  //   childrenLocations: [],
  // }];

  subjectIsSending = false;
  levelIsSending = false;
  languageIsSending = false;
  locationIsSending = false;

  subjectForm = this.fb.group({
    subject: ['', [Validators.required]],
  });

  levelForm = this.fb.group({
    level: ['', [Validators.required]],
  });

  languageForm = this.fb.group({
    language: ['', [Validators.required]],
  });

  locationForm = this.fb.group({
    location: ['', [Validators.required]],
    parentLocation: []
  })

  constructor(
    private fb: FormBuilder,
    private suggestionsService: SuggestionsService,
    private snackBar: MatSnackBar,
    private dictionariesService: DictionariesService) { }

  ngOnInit(): void {
    this.dictionariesService.getLocations().subscribe(locations => this.locations = locations);
  }

  get subjectCtrl() {
    return this.subjectForm.get('subject') as FormControl;
  }

  get levelCtrl() {
    return this.levelForm.get('level') as FormControl;
  }

  get languageCtrl() {
    return this.languageForm.get('language') as FormControl;
  }

  get locationCtrl() {
    return this.locationForm.get('location') as FormControl;
  }

  get parentLocationCtrl() {
    return this.locationForm.get('parentLocation') as FormControl;
  }

  sendSubject(): void {
    if (this.subjectForm.invalid) {
      return;
    }

    this.subjectIsSending = true;
    const subjectRequest = new SubjectRequest(this.subjectCtrl.value);

    console.log(subjectRequest.name);

    const saveObservable = this.suggestionsService.sendSubject(subjectRequest);

    saveObservable.subscribe(
      () => {
        this.subjectIsSending = false;
        this.snackBar.open("Wysłano pomyślnie.", "OK", {duration: 5000});
      }, (error) => {
        this.subjectIsSending = false;
        this.snackBar.open("Wysłanie nie powiodło się.", "OK", {duration: 5000});
      });
  }

  sendLevel(): void {
    if (this.levelForm.invalid) {
      return;
    }

    this.levelIsSending = true;
    const levelRequest = new LevelRequest(this.levelCtrl.value);

    console.log(levelRequest.name);

    const saveObservable = this.suggestionsService.sendLevel(levelRequest);

    saveObservable.subscribe(
      () => {
        this.levelIsSending = false;
        this.snackBar.open("Wysłano pomyślnie.", "OK", {duration: 5000});
      }, (error) => {
        this.levelIsSending = false;
        this.snackBar.open("Wysłanie nie powiodło się.", "OK", {duration: 5000});
      });
  }

  sendLanguage(): void {
    if (this.languageForm.invalid) {
      return;
    }

    this.languageIsSending = true;
    const languageRequest = new LanguageRequest(this.languageCtrl.value);

    console.log(languageRequest.name);

    const saveObservable = this.suggestionsService.sendLanguage(languageRequest);

    saveObservable.subscribe(
      () => {
        this.languageIsSending = false;
        this.snackBar.open("Wysłano pomyślnie.", "OK", {duration: 5000});
      }, (error) => {
        this.languageIsSending = false;
        this.snackBar.open("Wysłanie nie powiodło się.", "OK", {duration: 5000});
      });
  }

  sendLocation(): void {
    if (this.locationForm.invalid) {
      return;
    }

    this.locationIsSending = true;
    const locationRequest = new LocationRequest(this.locationCtrl.value, this.parentLocationCtrl.value);

    console.log(locationRequest.name);

    const saveObservable = this.suggestionsService.sendLocation(locationRequest);

    saveObservable.subscribe(
      () => {
        this.locationIsSending = false;
        this.snackBar.open("Wysłano pomyślnie.", "OK", {duration: 5000});
      }, (error) => {
        this.locationIsSending = false;
        this.snackBar.open("Wysłanie nie powiodło się.", "OK", {duration: 5000});
      });
  }
}
