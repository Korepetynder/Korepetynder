import { Component, OnInit } from '@angular/core';

import { Gallery, GalleryItem, ImageItem, ThumbnailsPosition, ImageSize } from 'ng-gallery';

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

  getPhotoGallery(): void {
    this.items = this.imageData.map(item => new ImageItem({ src: item.srcUrl, thumb: item.previewUrl }));
  }

}


const data = [
  {
    srcUrl: 'https://preview.ibb.co/jrsA6R/img12.jpg',
    previewUrl: 'https://preview.ibb.co/jrsA6R/img12.jpg'
  },
  {
    srcUrl: 'https://preview.ibb.co/kPE1D6/clouds.jpg',
    previewUrl: 'https://preview.ibb.co/kPE1D6/clouds.jpg'
  },
  {
    srcUrl: 'https://preview.ibb.co/mwsA6R/img7.jpg',
    previewUrl: 'https://preview.ibb.co/mwsA6R/img7.jpg'
  },
  {
    srcUrl: 'https://preview.ibb.co/kZGsLm/img8.jpg',
    previewUrl: 'https://preview.ibb.co/kZGsLm/img8.jpg'
  }
];
