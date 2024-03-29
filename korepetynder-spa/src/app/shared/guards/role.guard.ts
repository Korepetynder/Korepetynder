import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { UserType } from '../models/userType';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate, CanLoad {
  constructor(private userService: UserService, private router: Router) { }

  private checkRole(expectedRole: UserType): Observable<boolean | UrlTree> {
    return this.userService.getUserType().pipe(
      map(userType => {
        if (expectedRole === UserType.Uninitialized) {
          return (userType === 0 ? true : this.redirectToDefaultPage(userType));
        }
        return ((userType & expectedRole) !== 0 ? true : this.redirectToDefaultPage(userType));
      })
    );
  }

  private redirectToDefaultPage(userType: UserType): UrlTree {
    if ((userType & UserType.Student) !== 0) {
      return this.router.parseUrl('/home');
    } else if ((userType & UserType.Initialized) !== 0) {
      return this.router.parseUrl('/settings');
    } else {
      return this.router.parseUrl('/settings/init');
    }
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (route.data['expectedRole'] === undefined) {
      throw new Error('Missing expectedRole');
    }

    let expectedRole = route.data['expectedRole'] as UserType;
    return this.checkRole(expectedRole);
  }

  canLoad(
    route: Route,
    segments: UrlSegment[]): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (route.data?.['expectedRole'] === undefined) {
      throw new Error('Missing expectedRole');
    }

    let expectedRole = route.data['expectedRole'] as UserType;
    return this.checkRole(expectedRole);
  }
}
