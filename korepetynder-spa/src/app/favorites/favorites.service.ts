import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TutorDetails } from '../home/tutor-card/tutorDetails';

@Injectable({
  providedIn: 'root'
})
export class FavoritesService {
  private favoriteApiUrl = environment.apiUrl + '/student/favorite';

  constructor(private httpClient: HttpClient) { }

  getFavorites(): Observable<TutorDetails[]> {
    return this.httpClient.get<TutorDetails[]>(this.favoriteApiUrl);
  }

  addToFavorites(id: string): Observable<any> {
    return this.httpClient.post(`${this.favoriteApiUrl}/${id}`, null);
  }

  removeFromFavorites(id: string): Observable<any> {
    return this.httpClient.delete(`${this.favoriteApiUrl}/${id}`);
  }
}
