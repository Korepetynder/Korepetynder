import { Component, OnInit } from '@angular/core';

import { TutorDetails } from "../../home/tutor-card/tutorDetails";
import { MockTutors } from '../../home/tutor-card/mock-tutors';

@Component({
  selector: 'app-favorite-tutor-card',
  templateUrl: './favorite-tutor-card.component.html',
  styleUrls: ['./favorite-tutor-card.component.scss']
})
export class FavoriteTutorCardComponent implements OnInit {
  tutor: TutorDetails = MockTutors[0];

  constructor() { }

  ngOnInit(): void {
  }

  handleDeleteButton(): void {

  }
}
