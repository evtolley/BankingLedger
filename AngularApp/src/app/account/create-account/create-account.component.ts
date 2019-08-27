import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { AccountService } from 'src/app/swagger-proxy/services';
import { CreateAccountDto } from 'src/app/swagger-proxy/models';
import { takeWhile, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateAccountComponent implements OnInit, OnDestroy {

  constructor(private readonly accountService: AccountService,
    private router: Router,
    private toastr: ToastrService) { }

    componentIsActive = true;
    model: CreateAccountDto = { email: null, password: null, confirmPassword: null};


    createAccount() {
      this.accountService.AccountCreateAccount(this.model).pipe(
        takeWhile(() => this.componentIsActive),
        map(res => {
          localStorage.setItem('token', res.loginData.token);
          this.toastr.success(`Welcome ${res.loginData.email}!`);
          this.router.navigate(['/ledger']);
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
