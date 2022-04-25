import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserService } from '../shared/services/user.service';
import { UserRequest } from './models/requests/userRequest';
import { UserResponse } from './models/responses/user';

@Injectable({
  providedIn: 'root'
})
export class UserSettingsService {
  private apiUrl = environment.apiUrl + '/user';

  constructor(private httpClient: HttpClient, private userService: UserService) { }

  getUser(): Observable<UserResponse> {
    return this.httpClient.get<UserResponse>(this.apiUrl);
  }

  createUser(userRequest: UserRequest): Observable<UserResponse> {
    return this.httpClient.post<UserResponse>(this.apiUrl, userRequest).pipe(
      tap(() => this.userService.refreshUserType())
    );
  }

  updateUser(userRequest: UserRequest): Observable<UserResponse> {
    return this.httpClient.put<UserResponse>(this.apiUrl, userRequest);
  }
}
