import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { Gallery, GalleryItem, ImageItem } from 'ng-gallery';

import { TutorDetails } from './tutorDetails';
import { OpinionPopupComponent } from "../../opinion-popup/opinion-popup.component";
import { MatDialog } from "@angular/material/dialog";
import { TutorFindService } from '../tutor-find.service';
import { FavoritesService } from 'src/app/favorites/favorites.service';

@Component({
  selector: 'app-tutor-card',
  templateUrl: './tutor-card.component.html',
  styleUrls: ['./tutor-card.component.scss']
})
export class TutorCardComponent implements OnInit {
  @Input() tutor!: TutorDetails;

  @Output() nextTutor = new EventEmitter<void>();

  panelOpenState = false;
  isFavorite = false;

  // constructor() { }
  imageData = data;
  items: GalleryItem[] = this.imageData.map(item => new ImageItem({ src: item.srcUrl, thumb: item.previewUrl }));

  constructor(
    public gallery: Gallery,
    public dialog: MatDialog,
    private favoritesService: FavoritesService,
    private tutorFindService: TutorFindService) {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(OpinionPopupComponent, {
      width: '40em',
      data: {
        tutorId: this.tutor.id
      }
      // data: {name: this.name, animal: this.animal},
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
  }


  ngOnInit(): void {
    this.getPhotoGallery();
  }

  getNextTutor(): void {
    if (!this.isFavorite) {
      this.tutorFindService.discardTutor(this.tutor.id).subscribe(() => console.log('Discarded'));
    }
    this.reset();
    this.getPhotoGallery();
    this.nextTutor.emit();
  }

  handleFavoritesButton(): void {
    const favoriteObservable = this.isFavorite
      ? this.favoritesService.removeFromFavorites(this.tutor.id)
      : this.favoritesService.addToFavorites(this.tutor.id);
    this.isFavorite = !this.isFavorite;

    favoriteObservable.subscribe(() => {
      console.log('Handled favorite add/remove');
    });
  }

  getPhotoGallery(): void {
    this.items = this.imageData.map(item => new ImageItem({ src: item.srcUrl, thumb: item.previewUrl }));
  }

  reset(): void {
    this.getPhotoGallery();
    this.panelOpenState = false;
    this.isFavorite = false;
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
