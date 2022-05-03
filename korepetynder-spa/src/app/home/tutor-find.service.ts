import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TutorDetails } from './tutor-card/tutorDetails';

@Injectable({
  providedIn: 'root'
})
export class TutorFindService {
  private apiUrl = environment.apiUrl + '/student/tutors';

  constructor(private httpClient: HttpClient) { }

  getTutors(): Observable<TutorDetails[]> {
    return this.httpClient.get<TutorDetails[]>(this.apiUrl);
  }
}
