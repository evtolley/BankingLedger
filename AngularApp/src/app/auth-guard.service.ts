import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) {
  }

  /**
   * Only allows users with tokens to access requested route
   */
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
      if (localStorage.getItem('token')) {
          return of(true);
      }

      // if we get here, token does not exist. Redirect to login screen.
    this.router.navigate(['/account']);
  }
}
