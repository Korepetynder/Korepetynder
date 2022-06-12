import { Component, OnInit } from '@angular/core';
import { TutorDetails } from "../home/tutor-card/tutorDetails";
import { MockTutors } from '../home/tutor-card/mock-tutors';


@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss'],
})
export class RatingComponent implements OnInit {
  tutor: TutorDetails = MockTutors[0];
  score = 1;

  constructor() { }

  ngOnInit(): void {
  }

  handleSendReview() {

  }

}
