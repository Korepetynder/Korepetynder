import { Component, OnInit } from '@angular/core';

//enum SettingsType {
//  General,
//  Student,
//  Tutor
//}

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {
  //settingsType: SettingsType = SettingsType.General;

  constructor() { }

  ngOnInit(): void {
  }

}
