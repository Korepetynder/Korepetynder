import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Language } from './models/responses/language';
import { Level } from './models/responses/level';
import { Location } from './models/responses/location';
import { Subject } from './models/responses/subject';

@Injectable({
  providedIn: 'root'
})
export class DictionariesService {
  private apiUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  getLanguages(): Observable<Language[]> {
    return this.httpClient.get<Language[]>(this.apiUrl + '/languages');
  }

  getLevels(): Observable<Level[]> {
    return this.httpClient.get<Level[]>(this.apiUrl + '/levels');
  }

  getLocations(): Observable<Location[]> {
    return this.httpClient.get<Location[]>(this.apiUrl + '/locations');
  }

  getSubjects(): Observable<Subject[]> {
    return this.httpClient.get<Subject[]>(this.apiUrl + '/subjects');
  }

  getLanguagesToApprove(): Observable<Language[]> {
    return this.httpClient.get<Language[]>(this.apiUrl + '/languages/manage');
  }

  getLevelsToApprove(): Observable<Level[]> {
    return this.httpClient.get<Level[]>(this.apiUrl + '/levels/manage');
  }

  getLocationsToApprove(): Observable<Location[]> {
    return this.httpClient.get<Location[]>(this.apiUrl + '/locations/manage');
  }

  getSubjectsToApprove(): Observable<Subject[]> {
    return this.httpClient.get<Subject[]>(this.apiUrl + '/subjects/manage');
  }
}
