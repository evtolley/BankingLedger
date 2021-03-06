import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { AccountService } from 'src/app/swagger-proxy/services';
import { CreateAccountDto } from 'src/app/swagger-proxy/models';
import { takeWhile, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/auth.service';

@Component({
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateAccountComponent implements OnInit, OnDestroy {

  constructor(private readonly accountApiProxy: AccountService,
    private readonly authService: AuthService,
    private readonly toastr: ToastrService) { }

    submitClicked = false;
    componentIsActive = true;
    model: CreateAccountDto = { email: null, password: null, confirmPassword: null};


    createAccount() {
      this.submitClicked = true;
      this.accountApiProxy.AccountCreateAccount(this.model).pipe(
        takeWhile(() => this.componentIsActive),
        map(res => {
          this.authService.login(res.loginData.token, res.loginData.email);
        }),
        catchError(res => {
          if (res.error.title) {
            this.toastr.error(res.error.title);
          } else {
            this.toastr.error('Oops, something went wrong. Please try again.');
          }
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
