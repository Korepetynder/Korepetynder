import { Component, Input } from '@angular/core';
import { Rating } from 'src/app/shared/models/rating';

@Component({
  selector: 'app-opinion-card',
  templateUrl: './opinion-card.component.html',
  styleUrls: ['./opinion-card.component.scss']
})
export class OpinionCardComponent {
  @Input() rating!: Rating;

  constructor() { }

}
