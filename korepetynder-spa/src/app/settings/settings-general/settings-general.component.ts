import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings-general',
  templateUrl: './settings-general.component.html',
  styleUrls: ['./settings-general.component.scss']
})
export class SettingsGeneralComponent {

  constructor(public router: Router) { }

}
