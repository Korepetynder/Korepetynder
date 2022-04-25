import { Component, OnInit } from '@angular/core';

import { Tutor } from './tutor';
import { MockTutors } from './mock-tutors';

@Component({
  selector: 'app-tutor-card',
  templateUrl: './tutor-card.component.html',
  styleUrls: ['./tutor-card.component.scss']
})
export class TutorCardComponent implements OnInit {
  tutor: Tutor = MockTutors[0];

  constructor() { }

  ngOnInit(): void {
    this.getFirstTutor();
  }

  getFirstTutor(): void {
    this.tutor = MockTutors[0];
  }

  getNextTutor(): void {
    if (this.tutor == undefined) {
      this.getFirstTutor();
    }
    else {
      let id = this.tutor.id;
      id = (id + 1) % 4;

      this.tutor = MockTutors[id];
    }
  }

  addToFavorites(): void {

  }
}
