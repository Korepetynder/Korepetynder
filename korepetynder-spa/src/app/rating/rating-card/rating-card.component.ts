import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RatingRequest } from 'src/app/shared/models/ratingRequest';
import { RatingService } from 'src/app/shared/services/rating.service';
import { TutorDetails } from "../../home/tutor-card/tutorDetails";

@Component({
  selector: 'app-rating-card',
  templateUrl: './rating-card.component.html',
  styleUrls: ['./rating-card.component.scss']
})
export class RatingCardComponent {
  @Input() tutor!: TutorDetails;

  @Output() ratingSent = new EventEmitter<void>();

  constructor(
    private ratingService: RatingService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar) { }

  ratingForm = this.formBuilder.group({
    score: [null, Validators.required],
    comment: ['']
  });

  get score(): FormControl { return this.ratingForm.get('score')! as FormControl; }
  get comment(): FormControl { return this.ratingForm.get('comment')! as FormControl; }

  handleSendRating(): void {
    console.log(this.tutor);
    const ratingRequest = new RatingRequest(this.score.value, this.comment.value);
    this.ratingService.createRating(this.tutor.id, ratingRequest).subscribe(() => {
      this.snackBar.open('Opinia została wysłana.', 'OK', { duration: 5000 });
      this.ratingSent.emit();
    });
  }
}
