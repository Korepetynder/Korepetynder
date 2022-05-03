import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserService } from '../shared/services/user.service';
import { TutorLessonRequest } from './models/requests/tutorLessonRequest';
import { TutorRequest } from './models/requests/tutorRequest';
import { Tutor } from './models/responses/tutor';
import { TutorLesson } from './models/responses/tutorLesson';

@Injectable({
  providedIn: 'root'
})
export class TutorSettingsService {
  private apiUrl = environment.apiUrl + '/tutor';

  constructor(private httpClient: HttpClient, private userService: UserService) { }

  getTutor(): Observable<Tutor> {
    return this.httpClient.get<Tutor>(this.apiUrl);
  }

  createTutor(tutorRequest: TutorRequest): Observable<Tutor> {
    return this.httpClient.post<Tutor>(this.apiUrl, tutorRequest).pipe(
      tap(() => this.userService.refreshUserType())
    );
  }

  updateTutor(tutorRequest: TutorRequest): Observable<Tutor> {
    return this.httpClient.put<Tutor>(this.apiUrl, tutorRequest);
  }

  deleteTutor(): Observable<any> {
    return this.httpClient.delete(this.apiUrl).pipe(
      tap(() => this.userService.refreshUserType())
    );
  }

  getLessons(): Observable<TutorLesson[]> {
    return this.httpClient.get<TutorLesson[]>(this.apiUrl + '/lessons');
  }

  createLesson(tutorLessonRequest: TutorLessonRequest): Observable<TutorLesson> {
    return this.httpClient.post<TutorLesson>(this.apiUrl + '/lessons', tutorLessonRequest);
  }

  updateLesson(id: number, tutorLessonRequest: TutorLessonRequest): Observable<TutorLesson> {
    return this.httpClient.put<TutorLesson>(`${this.apiUrl}/lessons/${id}`, tutorLessonRequest);
  }

  deleteLesson(id: number): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}/lessons/${id}`);
  }
}
