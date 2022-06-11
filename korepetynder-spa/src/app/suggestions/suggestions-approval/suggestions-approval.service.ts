import { HttpClient, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LanguageRequest } from '../models/requests/languageRequest';
import { LevelRequest } from '../models/requests/levelRequest';
import { LocationRequest } from '../models/requests/locationRequest';
import { SubjectRequest } from "../models/requests/subjectRequest";
import { Language } from '../models/responses/language';
import { Level } from '../models/responses/level';
import { Subject } from "../models/responses/subject";

@Injectable({
  providedIn: 'root'
})
export class SuggestionsApprovalService {
  private apiUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  approveSubject(id: number): Observable<any> {
    return this.httpClient.post(this.apiUrl + `/subjects/manage/${id}`, {});
  }

  removeSubject(id: number): Observable<any> {
    return this.httpClient.delete(this.apiUrl + `/subjects/manage/${id}`, {});
  }

  approveLevel(id: number): Observable<any> {
    return this.httpClient.post(this.apiUrl + `/levels/manage/${id}/0`, {});
  }

  removeLevel(id: number): Observable<any> {
    return this.httpClient.delete(this.apiUrl + `/levels/manage/${id}`);
  }

  approveLanguage(id: number): Observable<any> {
    return this.httpClient.post(this.apiUrl + `/languages/manage/${id}`, {});
  }

  removeLanguage(id: number): Observable<any> {
    return this.httpClient.delete(this.apiUrl + `/languages/manage/${id}`);
  }
}
