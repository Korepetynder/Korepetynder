import { HttpClient, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserType } from '../shared/models/userType';
import { LanguageRequest } from './models/requests/languageRequest';
import { LevelRequest } from './models/requests/levelRequest';
import { LocationRequest } from './models/requests/locationRequest';
import { SubjectRequest } from "./models/requests/subjectRequest";
import { Language } from './models/responses/language';
import { Level } from './models/responses/level';
import { Subject } from "./models/responses/subject";

@Injectable({
  providedIn: 'root'
})
export class SuggestionsService {
  private apiUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  sendSubject(subjectRequest: SubjectRequest): Observable<Subject> {
    return this.httpClient.post<Subject>(this.apiUrl + '/subjects', subjectRequest)
      .pipe(
        catchError((err) => {
          return throwError(() => new Error(err));
        })
      )
  }

  sendLevel(levelRequest: LevelRequest): Observable<Level> {
    return this.httpClient.post<Level>(this.apiUrl + '/levels', levelRequest)
      .pipe(
        catchError((err) => {
          return throwError(() => new Error(err));
        })
      )
  }

  sendLanguage(languageRequest: LanguageRequest): Observable<Language> {
    return this.httpClient.post<Language>(this.apiUrl + '/languages', languageRequest)
      .pipe(
        catchError((err) => {
          return throwError(() => new Error(err));
        })
      )
  }

  sendLocation(locationRequest: LocationRequest): Observable<Location> {
    return this.httpClient.post<Location>(this.apiUrl + '/locations', locationRequest)
      .pipe(
        catchError((err) => {
          return throwError(() => new Error(err));
        })
      )
  }
}
