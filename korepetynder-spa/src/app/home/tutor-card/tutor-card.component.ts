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
export class TutorCardComponent {
  @Input() tutor!: TutorDetails;
  @Input() images: ImageItem[] = [];

  @Output() nextTutor = new EventEmitter<void>();

  panelOpenState = false;
  isFavorite = false;

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

  getNextTutor(): void {
    if (!this.isFavorite) {
      this.tutorFindService.discardTutor(this.tutor.id).subscribe(() => console.log('Discarded'));
    }
    this.reset();
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

  reset(): void {
    //this.getPhotoGallery();
    this.panelOpenState = false;
    this.isFavorite = false;
  }
}
