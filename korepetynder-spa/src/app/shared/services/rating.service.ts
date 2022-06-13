import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Rating } from '../models/rating';
import { environment } from 'src/environments/environment';
import { RatingRequest } from '../models/ratingRequest';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  private apiUrl = environment.apiUrl + '/tutor';

  constructor(private httpClient: HttpClient) { }

  getRatings(tutorId: string): Observable<Rating[]> {
    return this.httpClient.get<Rating[]>(`${this.apiUrl}/${tutorId}/comments`);
  }

  createRating(tutorId: string, rating: RatingRequest): Observable<any> {
    return this.httpClient.post(`${this.apiUrl}/${tutorId}/comments`, rating);
  }
}
