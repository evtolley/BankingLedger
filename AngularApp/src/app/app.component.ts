import { Component, OnInit, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { AccountService } from './swagger-proxy/services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'ETolley Ledger';
  componentIsActive = false;

  constructor(private readonly transactionService: AccountService) {}

  ngOnInit() {
  }

  ngOnDestroy() {
    this.componentIsActive = false;
  }
}
