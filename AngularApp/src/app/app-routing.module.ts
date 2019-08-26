import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth-guard.service';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        loadChildren: './account/account.module#AccountModule'
      },
      {
        path: 'ledger',
        loadChildren: './ledger/ledger.module#LedgerModule',
        canActivate: [AuthGuard]
      },
    ]
  },
  {
    path: '**',
    loadChildren: './account/account.module#AccountModule'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
