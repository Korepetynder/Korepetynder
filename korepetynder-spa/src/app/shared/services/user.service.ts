import { HttpClient, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MsalBroadcastService } from '@azure/msal-angular';
import { InteractionStatus } from '@azure/msal-browser';
import { catchError, filter, map, Observable, of, ReplaySubject, switchMap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserType } from '../models/userType';

interface UserRolesResponse {
  isStudent: boolean;
  isTutor: boolean;
  isAdmin: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;

  private userTypeSubject = new ReplaySubject<UserType>(1);

  constructor(private httpClient: HttpClient, private msalBroadcastService: MsalBroadcastService) {
    this.refreshUserType();
  }

  private mapToUserType(roles: UserRolesResponse): UserType {
    let userType = UserType.Initialized;
    if (roles.isStudent) {
      userType |= UserType.Student;
    }
    if (roles.isTutor) {
      userType |= UserType.Tutor;
    }
    if (roles.isAdmin) {
      userType |= UserType.Admin;
    }

    return userType;
  }

  private handleHttpError(error: HttpErrorResponse): Observable<UserType> {
    if (error.status === HttpStatusCode.NotFound) {
      return of(UserType.Uninitialized);
    }
    return throwError(() => new Error("Can't get user type"));
  }

  getUserType(): Observable<UserType> {
    return this.userTypeSubject.asObservable();
  }

  refreshUserType(): void {
    this.msalBroadcastService.inProgress$.pipe(
      filter((status: InteractionStatus) => status === InteractionStatus.None),
      switchMap(() => this.httpClient.get<UserRolesResponse>(this.apiUrl + '/user/roles').pipe(
        map(this.mapToUserType),
        catchError(this.handleHttpError)
      ))
    ).subscribe(userType => this.userTypeSubject.next(userType));
  }

  isInitialized(): Observable<boolean> {
    return this.getUserType().pipe(
      map(userType => (userType & UserType.Initialized) !== 0)
    );
  }

  isStudent(): Observable<boolean> {
    return this.getUserType().pipe(
      map(userType => (userType & UserType.Student) !== 0)
    );
  }

  isTutor(): Observable<boolean> {
    return this.getUserType().pipe(
      map(userType => (userType & UserType.Tutor) !== 0)
    );
  }

  isAdmin(): Observable<boolean> {
    return this.getUserType().pipe(
      map(userType => (userType & UserType.Admin) !== 0)
    );
  }
}
