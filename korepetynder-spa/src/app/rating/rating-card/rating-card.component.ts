import { Component, OnInit } from '@angular/core';
import { TutorDetails } from "../../home/tutor-card/tutorDetails";
import { MockTutors } from "../../home/tutor-card/mock-tutors";

@Component({
  selector: 'app-rating-card',
  templateUrl: './rating-card.component.html',
  styleUrls: ['./rating-card.component.scss']
})
export class RatingCardComponent implements OnInit {
  tutor: TutorDetails = MockTutors[0];

  constructor() { }

  ngOnInit(): void {
  }

  handleSendReview() {

  }
}
