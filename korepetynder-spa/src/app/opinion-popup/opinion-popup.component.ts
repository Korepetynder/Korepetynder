import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Rating } from '../shared/models/rating';
import { RatingService } from '../shared/services/rating.service';

export interface DialogData {
  tutorId: string;
}

@Component({
  selector: 'app-opinion-popup',
  templateUrl: './opinion-popup.component.html',
  styleUrls: ['./opinion-popup.component.scss']
})
export class OpinionPopupComponent implements OnInit {
  ratings: Rating[] = [];

  constructor(
    public dialogRef: MatDialogRef<OpinionPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private ratingService: RatingService
  ) { }

  ngOnInit(): void {
    this.ratingService.getRatings(this.data.tutorId).subscribe(ratings =>
      this.ratings = ratings);
  }

  closeOnClick(): void {
    this.dialogRef.close();
  }

}
