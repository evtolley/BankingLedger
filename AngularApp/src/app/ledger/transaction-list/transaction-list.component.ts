import { Component, OnInit, ChangeDetectionStrategy, Input, OnDestroy } from '@angular/core';
import { LedgerTransactionDto } from 'src/app/swagger-proxy/models';
import { LedgerService } from '../ledger.service';
import { takeWhile, catchError } from 'rxjs/operators';
import { of, Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TransactionListComponent implements OnInit, OnDestroy {

  constructor(private readonly ledgerService: LedgerService, private readonly toastr: ToastrService) { }

  componentIsActive = true;
  transactionSub$ : Subscription;

  @Input()
  transactions: LedgerTransactionDto[];

  ngOnInit() {
    this.loadTransactions();
  }

  loadTransactions() {
    this.transactionSub$ = this.ledgerService.loadTransactions(15)
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
