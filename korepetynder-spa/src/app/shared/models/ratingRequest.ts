export class RatingRequest {
  score: number;
  comment: string;

  constructor(score: number, comment: string) {
    this.score = score;
    this.comment = comment;
  }
}
