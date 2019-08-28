import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { AccountService } from 'src/app/swagger-proxy/services';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginAttemptDto } from 'src/app/swagger-proxy/models';
import { takeWhile, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit, OnDestroy {

  constructor(private readonly accountService: AccountService,
    private readonly router: Router,
    private readonly toastr: ToastrService) { }
    componentIsActive = true;
    model: LoginAttemptDto = { email: null, password: null };


    login() {
      this.accountService.AccountLogin(this.model).pipe(
        takeWhile(() => this.componentIsActive),
        map(res => {
          localStorage.setItem('token', res.token);
          this.toastr.success(`Welcome ${res.email}!`);
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
