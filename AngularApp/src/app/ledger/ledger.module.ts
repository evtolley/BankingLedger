import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LedgerRoutingModule } from './ledger-routing.module';
import { LedgerHomeComponent } from './ledger-home/ledger-home.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { NewTransactionFormComponent } from './new-transaction-form/new-transaction-form.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [LedgerHomeComponent, TransactionListComponent, NewTransactionFormComponent],
  imports: [
    CommonModule,
    LedgerRoutingModule,
    FormsModule
  ]
})
export class LedgerModule { }
