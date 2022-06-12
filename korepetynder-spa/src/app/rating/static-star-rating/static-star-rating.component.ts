import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-static-star-rating',
  templateUrl: './static-star-rating.component.html',
  styleUrls: ['./static-star-rating.component.scss']
})
export class StaticStarRatingComponent implements OnInit {
  starRatingAverage = 1.4;

  constructor() { }

  ngOnInit(): void {
  }

}
