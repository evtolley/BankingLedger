import { Component, OnInit, ChangeDetectionStrategy, Input } from '@angular/core';
import { LedgerTransactionDto } from 'src/app/swagger-proxy/models';

@Component({
  selector: 'transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TransactionListComponent implements OnInit {

  constructor() { }

  @Input()
  transactions: LedgerTransactionDto[];

  ngOnInit() {
  }
}
