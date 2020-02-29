import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LedgerRoutingModule } from './ledger-routing.module';
import { LedgerHomeComponent } from './ledger-home/ledger-home.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { TransactionFormComponent } from './transaction-form/transaction-form.component';
import { FormsModule } from '@angular/forms';
import { TransactionComponent } from './transaction/transaction.component';

@NgModule({
  declarations: [
    LedgerHomeComponent, 
    TransactionComponent, 
    TransactionListComponent, 
    TransactionFormComponent
  ],
  imports: [
    CommonModule,
    LedgerRoutingModule,
    FormsModule
  ]
})
export class LedgerModule { }
