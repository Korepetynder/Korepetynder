import { HttpClient, HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MsalBroadcastService } from '@azure/msal-angular';
import { InteractionStatus } from '@azure/msal-browser';
import { catchError, filter, map, Observable, of, shareReplay, switchMap, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserType } from '../models/userType';

interface UserRolesResponse {
  isStudent: boolean;
  isTeacher: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;
  private userType$: Observable<UserType>;

  constructor(private httpClient: HttpClient, private msalBroadcastService: MsalBroadcastService) {
    this.userType$ = this.constructUserTypeObservable();
  }

  private constructUserTypeObservable(): Observable<UserType> {
    return this.msalBroadcastService.inProgress$.pipe(
      filter((status: InteractionStatus) => status === InteractionStatus.None),
      switchMap(() => this.httpClient.get<UserRolesResponse>(this.apiUrl + '/user/roles').pipe(
        map(this.mapToUserType),
        catchError(this.handleHttpError),
        shareReplay()
      ))
    );
  }

  private mapToUserType(roles: UserRolesResponse): UserType {
    let userType = UserType.Initialized;
    if (roles.isStudent) {
      userType |= UserType.Student;
    }
    if (roles.isTeacher) {
      userType |= UserType.Teacher;
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
    return this.userType$;
  }

  refreshUserType(): void {
    this.userType$ = this.constructUserTypeObservable();
  }
}
