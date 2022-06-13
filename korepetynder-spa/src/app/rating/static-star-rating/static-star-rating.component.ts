import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-static-star-rating',
  templateUrl: './static-star-rating.component.html',
  styleUrls: ['./static-star-rating.component.scss']
})
export class StaticStarRatingComponent {
  @Input() starRatingAverage!: number;

  constructor() { }
}
