import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LedgerHomeComponent } from './ledger-home/ledger-home.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'ledger'
  },
  {
    path: 'ledger',
    component: LedgerHomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LedgerRoutingModule { }
