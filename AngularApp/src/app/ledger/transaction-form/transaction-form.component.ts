import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { LedgerService } from '../ledger.service';
import { InputLedgerTransactionDto } from 'src/app/swagger-proxy/models';
import { takeWhile, catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NewTransactionFormComponent implements OnInit, OnDestroy {

  componentIsActive = true;
  model: InputLedgerTransactionDto = { amount: 0, transactionType: 1 };

  constructor(private readonly ledgerService: LedgerService, private readonly toastrService: ToastrService) { }

  addTransaction() {
    this.ledgerService.addTransaction(this.model).pipe(
      takeWhile(() => this.componentIsActive),
      map(res => {
        this.toastrService.success('Transaction successful!');
      }),
      catchError(res => {
        this.toastrService.error(res.error.title);
        return of();
      })
    ).subscribe();
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.componentIsActive = false;
  }
}
