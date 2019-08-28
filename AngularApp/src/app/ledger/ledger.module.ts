import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LedgerRoutingModule } from './ledger-routing.module';
import { LedgerHomeComponent } from './ledger-home/ledger-home.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';

@NgModule({
  declarations: [LedgerHomeComponent, TransactionListComponent],
  imports: [
    CommonModule,
    LedgerRoutingModule
  ]
})
export class LedgerModule { }
