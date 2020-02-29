import { Component, OnInit, ChangeDetectionStrategy, OnDestroy, Output, EventEmitter, Input } from '@angular/core';
import { LedgerService } from '../ledger.service';
import { InputLedgerTransactionDto, LedgerTransactionDto } from 'src/app/swagger-proxy/models';
import { takeWhile, catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TransactionFormComponent implements OnInit {

  constructor(private readonly ledgerService: LedgerService, private readonly toastrService: ToastrService) { }

  @Input()
  model: InputLedgerTransactionDto = { amount: 0, transactionType: 1 };

  @Output()
  submitForm: EventEmitter<InputLedgerTransactionDto> = new EventEmitter();

  save() {
    this.submitForm.emit(this.model);
  }

  ngOnInit() {
  }
}
