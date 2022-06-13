import { Component, OnInit } from '@angular/core';
import { ImageItem } from 'ng-gallery';

import { TutorDetails } from "../home/tutor-card/tutorDetails";
import { FavoritesService } from './favorites.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss']
})
export class FavoritesComponent implements OnInit {
  favorites: { tutor: TutorDetails, images: ImageItem[] }[] = [];

  constructor(private favoritesService: FavoritesService) { }

  ngOnInit(): void {
    this.favoritesService.getFavorites().subscribe(favorites => {
      this.favorites = favorites.map(favorite => ({
        tutor: favorite,
        images: favorite.multimediaFiles.map(
          multimediaFile => new ImageItem({ src: multimediaFile.url, thumb: multimediaFile.url }))
      }));
      console.log(favorites);
    });
  }
}
