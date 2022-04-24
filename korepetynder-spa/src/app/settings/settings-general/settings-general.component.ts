import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings-general',
  templateUrl: './settings-general.component.html',
  styleUrls: ['./settings-general.component.scss']
})
export class SettingsGeneralComponent {
  profileForm = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    birthDate: ['', [Validators.required]],
    street: ['', [Validators.required]],
    city: ['', [Validators.required]],
    houseNumber: ['', [Validators.required]],
    flatNumber: ['']
  });

  constructor(public router: Router, private fb: FormBuilder) { }

}
