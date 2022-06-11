import { Component, OnInit } from '@angular/core';

import { MockTutors } from '../home/tutor-card/mock-tutors';
import { TutorDetails } from "../home/tutor-card/tutorDetails";

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements OnInit {
  tutorsList : TutorDetails[] = MockTutors;

  constructor() { }

  ngOnInit(): void {
  }

}
