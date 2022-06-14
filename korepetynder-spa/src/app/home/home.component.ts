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
  isLoading = true;

  constructor(private tutorFindService: TutorFindService) { }

  ngOnInit(): void {
    this.getTutors();
  }

  nextTutor(): void {
    this.currentTutorIndex += 1;
    if (this.currentTutorIndex === this.tutors.length) {
      this.getTutors();
    } else {
      this.currentTutor = this.tutors[this.currentTutorIndex];
      this.setImages();
    }
  }

  getTutors(): void {
    this.isLoading = true;
    this.currentTutorIndex = 0;
    this.tutorFindService.getTutors().subscribe(tutors => {
      if (tutors.length > 0) {
        this.tutors = tutors;
        this.currentTutor = tutors[0];
        this.setImages();
      } else {
        this.currentTutor = undefined;
      }
      this.isLoading = false;
    });
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
