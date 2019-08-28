import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LedgerService } from '../ledger.service';
import { takeWhile, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  templateUrl: './ledger-home.component.html',
  styleUrls: ['./ledger-home.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LedgerHomeComponent implements OnInit, OnDestroy {

  constructor(private readonly ledgerService: LedgerService,
    private readonly toastr: ToastrService) { }

  componentIsActive = true;

  ngOnInit() {
    this.ledgerService.loadTransactions()
    .pipe(
      takeWhile(() => this.componentIsActive),
      catchError(res => {
        this.toastr.error(res.error.title);
        return of();
      })
    )
    .subscribe();
  }

  ngOnDestroy() {
    this.componentIsActive = false;
  }
}
