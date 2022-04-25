import { Component, OnInit } from '@angular/core';

import { Tutor } from './tutor';

@Component({
  selector: 'app-tutor-card',
  templateUrl: './tutor-card.component.html',
  styleUrls: ['./tutor-card.component.scss']
})
export class TutorCardComponent implements OnInit {
  tutor: Tutor | undefined;

  constructor() { }

  ngOnInit(): void {
  }

  getNextTutor(): void {

  }

  addToFavorites(): void {

  }
}
