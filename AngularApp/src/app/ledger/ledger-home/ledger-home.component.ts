import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LedgerService } from '../ledger.service';
import { takeWhile, catchError, map } from 'rxjs/operators';
import { of, Subscription } from 'rxjs';
import { AuthService } from 'src/app/auth.service';
import { InputLedgerTransactionDto } from 'src/app/swagger-proxy/models';

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
  createSubscription : Subscription;

  logout() {
    this.authService.logout();
  }

  ngOnInit() {
    this.ledgerService.getBalance()
    .pipe(
      takeWhile(() => this.componentIsActive),
      catchError(res => {
        this.toastr.error('Oops! something went wrong');
        return of();
      })
    ).subscribe();
  }

  addTransaction(model: InputLedgerTransactionDto) {
    this.createSubscription = this.ledgerService.addTransaction(model).pipe(
      takeWhile(() => this.componentIsActive),
      map(res => {
        this.toastr.success('Transaction successful!');
      }),
      catchError(res => {
        this.toastr.error(res.error.title);
        return of();
      })
    ).subscribe();
  }

  ngOnDestroy() {
    this.componentIsActive = false;
  }
}
