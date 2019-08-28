import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LedgerService } from '../ledger.service';
import { takeWhile, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { AuthService } from 'src/app/auth.service';

@Component({
  templateUrl: './ledger-home.component.html',
  styleUrls: ['./ledger-home.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LedgerHomeComponent implements OnInit, OnDestroy {

  constructor(private readonly ledgerService: LedgerService,
    private readonly toastr: ToastrService,
    private readonly authService: AuthService) { }

  componentIsActive = true;

  logout() {
    this.authService.logout();
  }

  ngOnInit() {
    this.ledgerService.loadTransactions()
    .pipe(
      takeWhile(() => this.componentIsActive),
      catchError(res => {
        this.toastr.error('Oops! something went wrong');
        return of();
      })
    )
    .subscribe();
  }

  ngOnDestroy() {
    this.componentIsActive = false;
  }
}
