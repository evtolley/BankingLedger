import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { AccountService } from 'src/app/swagger-proxy/services';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginAttemptDto } from 'src/app/swagger-proxy/models';
import { takeWhile, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { AuthService } from 'src/app/auth.service';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit, OnDestroy {

  constructor(private readonly accountApiProxy: AccountService,
    private readonly authService: AuthService,
    private readonly toastr: ToastrService) { }

    submitClicked = false;
    componentIsActive = true;
    model: LoginAttemptDto = { email: null, password: null };


    login() {
      this.submitClicked = true;
      this.accountApiProxy.AccountLogin(this.model).pipe(
        takeWhile(() => this.componentIsActive),
        map(res => {
          this.authService.login(res.token, res.email);
        }),
        catchError(res => {
          this.toastr.error(res.error.title);
          return of();
        })
      )
      .subscribe();
    }

    ngOnInit() {
    }

    ngOnDestroy() {
      this.componentIsActive = false;
    }
}
