import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserService } from '../shared/services/user.service';
import { StudentLessonRequest } from './models/requests/studentLessonRequest';
import { StudentRequest } from './models/requests/studentRequest';
import { Student } from './models/responses/student';
import { StudentLesson } from './models/responses/studentLesson';

@Injectable({
  providedIn: 'root'
})
export class StudentSettingsService {
  private apiUrl = environment.apiUrl + '/student';

  constructor(private httpClient: HttpClient, private userService: UserService) { }

  getStudent(): Observable<Student> {
    return this.httpClient.get<Student>(this.apiUrl);
  }

  createStudent(studentRequest: StudentRequest): Observable<Student> {
    return this.httpClient.post<Student>(this.apiUrl, studentRequest).pipe(
      tap(() => this.userService.refreshUserType())
    );
  }

  updateStudent(studentRequest: StudentRequest): Observable<Student> {
    return this.httpClient.put<Student>(this.apiUrl, studentRequest);
  }

  deleteStudent(): Observable<any> {
    return this.httpClient.delete(this.apiUrl).pipe(
      tap(() => this.userService.refreshUserType())
    );
  }

  getLessons(): Observable<StudentLesson[]> {
    return this.httpClient.get<StudentLesson[]>(this.apiUrl + '/lessons');
  }

  createLesson(studentLessonRequest: StudentLessonRequest): Observable<StudentLesson> {
    return this.httpClient.post<StudentLesson>(this.apiUrl + '/lessons', studentLessonRequest);
  }

  updateLesson(id: number, studentLessonRequest: StudentLessonRequest): Observable<StudentLesson> {
    return this.httpClient.put<StudentLesson>(`${this.apiUrl}/lessons/${id}`, studentLessonRequest);
  }

  deleteLesson(id: number): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}/lessons/${id}`);
  }
}
