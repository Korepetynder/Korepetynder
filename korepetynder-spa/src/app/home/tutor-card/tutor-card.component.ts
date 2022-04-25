import { Component, OnInit } from '@angular/core';

import { TutorDetails } from './tutorDetails';
import { MockTutors } from './mock-tutors';

@Component({
  selector: 'app-tutor-card',
  templateUrl: './tutor-card.component.html',
  styleUrls: ['./tutor-card.component.scss']
})
export class TutorCardComponent implements OnInit {
  tutor: TutorDetails = MockTutors[0];
  id: number = 0;

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
      this.id = (this.id + 1) % 4;

      this.tutor = MockTutors[this.id];
    }
  }

  addToFavorites(): void {

  }
}
