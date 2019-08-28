import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {
  }

  /**
   * Only allows users with tokens to access requested route
   */
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
      if (this.authService.isUserLoggedIn()) {
          return of(true);
      }

      // if we get here, token does not exist. Redirect to login screen.
    this.router.navigate(['/account']);
  }
}
