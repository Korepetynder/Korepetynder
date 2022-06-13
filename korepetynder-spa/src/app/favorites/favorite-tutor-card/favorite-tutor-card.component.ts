import { Component, Input } from '@angular/core';
import { TutorDetails } from "../../home/tutor-card/tutorDetails";
import { trigger, state, style, transition, animate } from '@angular/animations';
import { GalleryItem, ImageItem } from "ng-gallery";
import { MatDialog } from "@angular/material/dialog";
import { OpinionPopupComponent } from "../../opinion-popup/opinion-popup.component";
import { FavoritesService } from '../favorites.service';
import { RatingComponent } from 'src/app/rating/rating.component';

@Component({
  selector: 'app-favorite-tutor-card',
  templateUrl: './favorite-tutor-card.component.html',
  styleUrls: ['./favorite-tutor-card.component.scss'],
  animations: [
    trigger('flipState', [
      state('front', style({
        transform: 'rotateY(0)'
      })),
      state('back', style({
        transform: 'rotateY(179deg)'
      })),
      transition('front => back', animate('600ms ease-in')),
      transition('back => front', animate('600ms ease-out'))
    ])
  ]
})
export class FavoriteTutorCardComponent {
  @Input() tutor!: TutorDetails;

  isFavorite = true;
  visibleSide: string = 'front';
  imageData = data;
  items: GalleryItem[] = this.imageData.map(item => new ImageItem({ src: item.srcUrl, thumb: item.previewUrl }));

  constructor(public dialog: MatDialog, private favoritesService: FavoritesService) {}

  openDialog(): void {
    const dialogRef = this.dialog.open(OpinionPopupComponent, {
      width: '40em',
      data: {
        tutorId: this.tutor.id
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
  }

  handleAddReviewButton(): void {
    const dialogRef = this.dialog.open(RatingComponent, {
      data: {
        tutor: this.tutor
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
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

  toggleFlip() {
    this.visibleSide = (this.visibleSide == 'front') ? 'back' : 'front';
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
