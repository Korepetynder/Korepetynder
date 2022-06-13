import { Component, OnInit } from '@angular/core';

import { MockTutors } from '../home/tutor-card/mock-tutors';
import { TutorDetails } from "../home/tutor-card/tutorDetails";
import { FavoritesService } from './favorites.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements OnInit {
  tutorsList : TutorDetails[] = MockTutors;
  favorites: TutorDetails[] = [];

  constructor(private favoritesService: FavoritesService) { }

  ngOnInit(): void {
    this.favoritesService.getFavorites().subscribe(favorites => {
      this.favorites = favorites;
      console.log(favorites);
    });
  }
}
