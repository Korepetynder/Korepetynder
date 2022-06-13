import { Component, OnInit } from '@angular/core';
import { ImageItem } from 'ng-gallery';
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
  currentImages: ImageItem[] = [];

  constructor(private tutorFindService: TutorFindService) { }

  ngOnInit(): void {
    this.tutorFindService.getTutors().subscribe(tutors => {
      if (tutors.length > 0) {
        this.tutors = tutors;
        this.currentTutor = tutors[0];
        this.setImages();
      }
    });
  }

  nextTutor(): void {
    this.currentTutorIndex = (this.currentTutorIndex + 1) % this.tutors.length;
    this.currentTutor = this.tutors[this.currentTutorIndex];
    this.setImages();
  }

  setImages(): void {
    if (!this.currentTutor) {
      return;
    }
    this.currentImages = this.currentTutor?.multimediaFiles.map(
      multimediaFile => new ImageItem({ src: multimediaFile.url, thumb: multimediaFile.url }));
    console.log(this.currentImages);
  }
}
