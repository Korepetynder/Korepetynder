import { Component, Input } from '@angular/core';
import { TutorDetails } from "../../home/tutor-card/tutorDetails";
import { trigger, state, style, transition, animate } from '@angular/animations';
import { GalleryItem, ImageItem } from "ng-gallery";
import { MatDialog } from "@angular/material/dialog";
import { OpinionPopupComponent } from "../../opinion-popup/opinion-popup.component";
import { FavoritesService } from '../favorites.service';
import { RatingComponent } from 'src/app/rating/rating.component';
import { TutorLesson } from 'src/app/settings/models/responses/tutorLesson';

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
  @Input() images: ImageItem[] = [];

  isFavorite = true;
  visibleSide: string = 'front';

  constructor(public dialog: MatDialog, private favoritesService: FavoritesService) {}

  lessonDescription(lesson: TutorLesson) {
    return lesson.subject.name + ` (${lesson.cost} zł/h, ${lesson.frequency} h/tydz.): `
      + lesson.levels.map(l => l.name).join(", ") + " (" + lesson.languages.map(l => l.name).join(", ") + ")";
  }

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
