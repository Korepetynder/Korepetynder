import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LanguageRequest } from './models/requests/languageRequest';
import { LevelRequest } from './models/requests/levelRequest';
import { SubjectRequest } from './models/requests/subjectRequest';
import { SuggestionsService } from './suggestions.service';

@Component({
  selector: 'app-suggestions',
  templateUrl: './suggestions.component.html',
  styleUrls: ['./suggestions.component.scss']
})
export class SuggestionsComponent {
  subjectIsSending = false;
  levelIsSending = false;
  languageIsSending = false;

  subjectForm = this.fb.group({
    subject: ['', [Validators.required]],
  });

  levelForm = this.fb.group({
    level: ['', [Validators.required]],
  });

  languageForm = this.fb.group({
    language: ['', [Validators.required]],
  });

  constructor(
    private fb: FormBuilder,
    private suggestionsService: SuggestionsService,
    private snackBar: MatSnackBar) { }

  get subjectCtrl() {
    return this.subjectForm.get('subject') as FormControl;
  }

  get levelCtrl() {
    return this.levelForm.get('level') as FormControl;
  }

  get languageCtrl() {
    return this.languageForm.get('language') as FormControl;
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
}
