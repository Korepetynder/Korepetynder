import { Component, EventEmitter, Input, Output } from '@angular/core';

import { TutorDetails } from './tutorDetails';

@Component({
  selector: 'app-tutor-card',
  templateUrl: './tutor-card.component.html',
  styleUrls: ['./tutor-card.component.scss']
})
export class TutorCardComponent {
  @Input() tutor!: TutorDetails;

  @Output() nextTutor = new EventEmitter<void>();

  constructor() { }

  getNextTutor(): void {
    this.nextTutor.emit();
  }

  addToFavorites(): void {

  }
}
