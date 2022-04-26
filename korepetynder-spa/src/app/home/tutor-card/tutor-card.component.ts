import { Component, OnInit } from '@angular/core';

import { Gallery, GalleryItem, ImageItem } from 'ng-gallery';

import { TutorDetails } from './tutorDetails';
import { MockTutors } from './mock-tutors';

@Component({
  selector: 'app-tutor-card',
  templateUrl: './tutor-card.component.html',
  styleUrls: ['./tutor-card.component.scss']
})
export class TutorCardComponent implements OnInit {
  panelOpenState = false;
  tutor: TutorDetails = MockTutors[0];
  id: number = 0;

  // constructor() { }
  imageData = data;
  items: GalleryItem[] = this.imageData.map(item => new ImageItem({ src: item.srcUrl, thumb: item.previewUrl }));

  constructor(public gallery: Gallery) {
  }

  ngOnInit(): void {
    this.getFirstTutor();
    this.getPhotoGallery();
  }

  getFirstTutor(): void {
    this.tutor = MockTutors[0];
  }

  getNextTutor(): void {
    this.reset();

    if (this.tutor == undefined) {
      this.getFirstTutor();
    }
    else {
      this.id = (this.id + 1) % 3;

      this.tutor = MockTutors[this.id];
    }
  }

  handleFavoritesButton(): void {
    this.tutor.isFavorite = !this.tutor.isFavorite;
  }

  getPhotoGallery(): void {
    this.items = this.imageData.map(item => new ImageItem({ src: item.srcUrl, thumb: item.previewUrl }));
  }

  reset(): void {
    this.getPhotoGallery();
    this.panelOpenState = false;
  }
}


const data = [
  {
    srcUrl: 'https://cdn.pixabay.com/photo/2022/04/11/09/32/glacier-7125359_960_720.jpg',
    previewUrl: 'https://cdn.pixabay.com/photo/2022/04/11/09/32/glacier-7125359_960_720.jpg'
  },
  {
    srcUrl: 'https://cdn.pixabay.com/photo/2021/06/04/19/46/red-poppies-6310772_960_720.jpg',
    previewUrl: 'https://cdn.pixabay.com/photo/2021/06/04/19/46/red-poppies-6310772_960_720.jpg'
  },
  {
    srcUrl: 'https://cdn.pixabay.com/photo/2021/07/24/01/54/macaw-6488488_960_720.jpg',
    previewUrl: 'https://cdn.pixabay.com/photo/2021/07/24/01/54/macaw-6488488_960_720.jpg'
  },
  {
    srcUrl: 'https://cdn.pixabay.com/photo/2022/03/10/15/13/clouds-7060045_960_720.jpg',
    previewUrl: 'https://cdn.pixabay.com/photo/2022/03/10/15/13/clouds-7060045_960_720.jpg'
  },
];
