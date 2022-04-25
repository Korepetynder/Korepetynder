import { Component, OnInit } from '@angular/core';
import { TutorDetails } from './tutor-card/tutorDetails';
import { TutorFindService } from './tutor-find.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  private tutors: TutorDetails[] = [];
  currentTutorIndex = 0;
  currentTutor: TutorDetails | undefined;

  constructor(private tutorFindService: TutorFindService) { }

  ngOnInit(): void {
    this.tutorFindService.getTutors().subscribe(tutors => {
      if (tutors.length > 0) {
        this.tutors = tutors;
        this.currentTutor = tutors[0];
      }
    });
  }

  nextTutor(): void {
    this.currentTutorIndex = (this.currentTutorIndex + 1) % this.tutors.length;
    this.currentTutor = this.tutors[this.currentTutorIndex];
  }
}
