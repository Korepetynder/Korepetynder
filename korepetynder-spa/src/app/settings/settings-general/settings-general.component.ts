import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DateTime } from 'luxon';
import { UserRequest } from '../models/requests/userRequest';
import { UserSettingsService } from '../user-settings.service';

@Component({
  selector: 'app-settings-general',
  templateUrl: './settings-general.component.html',
  styleUrls: ['./settings-general.component.scss']
})
export class SettingsGeneralComponent implements OnInit {
  @Input() isEdit: boolean = false;

  @Output() completed = new EventEmitter<void>();

  isSaving = false;

  profileForm = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: [''],
    birthDate: ['', [Validators.required]]
  });

  get firstNameCtrl() { return this.profileForm.get('firstName') as FormControl; }
  get lastNameCtrl() { return this.profileForm.get('lastName') as FormControl; }
  get emailCtrl() { return this.profileForm.get('email') as FormControl; }
  get phoneNumberCtrl() { return this.profileForm.get('phoneNumber') as FormControl; }
  get birthDateCtrl() { return this.profileForm.get('birthDate') as FormControl; }

  constructor(public router: Router, private fb: FormBuilder, private userSetingsService: UserSettingsService) { }

  ngOnInit(): void {
    if (this.isEdit) {
      this.userSetingsService.getUser().subscribe(user => {
        this.profileForm.patchValue(user);
        this.birthDateCtrl.patchValue(DateTime.fromISO(user.birthDate, { zone: 'utc' }));
      })
    }
  }

  saveChanges(): void {
    if (this.profileForm.invalid) {
      return;
    }

    this.isSaving = true;
    const formValue = this.profileForm.value;
    const userRequest = new UserRequest(formValue.firstName, formValue.lastName, formValue.email,
      formValue.phoneNumber, formValue.birthDate);

    const saveObservable = this.isEdit
      ? this.userSetingsService.updateUser(userRequest)
      : this.userSetingsService.createUser(userRequest)

    saveObservable.subscribe(() => {
      this.isSaving = false;
      this.completed.emit();
    });
  }
}
