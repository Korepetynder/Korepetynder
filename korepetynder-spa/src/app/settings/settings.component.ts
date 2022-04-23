import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent {
  selectedTab: number = 0;

  constructor() { }

  setSelectedTab(id: number): void {
    this.selectedTab = id;
  }
}
