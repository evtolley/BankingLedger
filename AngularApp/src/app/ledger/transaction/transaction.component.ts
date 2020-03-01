import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { LedgerTransactionDto, InputLedgerTransactionDto } from 'src/app/swagger-proxy/models';
import { ToastrService } from 'ngx-toastr';
import { LedgerService } from '../ledger.service';
import { takeWhile, catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { TransactionViewMode } from './transactionviewmode.enum';

@Component({
  selector: 'transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit, OnDestroy {

  constructor(private readonly ledgerService: LedgerService, private readonly toastr: ToastrService) { }

  viewMode = TransactionViewMode.View;
  componentIsActive = true;

  @Input()
  transaction: LedgerTransactionDto;

  setViewMode(mode: TransactionViewMode) {
    this.viewMode = mode;
  }

  update(model: InputLedgerTransactionDto) {
    model.transactionId = this.transaction.transactionId;
    
    this.ledgerService.editTransaction(model)
    .pipe(
      takeWhile(() => this.componentIsActive),
      map(res => {
        this.toastr.success('Transaction updated');
      }),
      catchError(res => {
        this.toastr.error(res.error.title);
        return of();
      })
    )
    .subscribe()
    this.setViewMode(TransactionViewMode.View);
  }

  delete(model: InputLedgerTransactionDto) {
    console.log('delete clicked');
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.componentIsActive = false;
  }
}
