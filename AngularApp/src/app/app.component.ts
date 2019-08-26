import { Component, OnInit, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { AccountService } from './swagger-proxy/services';
import { takeWhile } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'AngularApp';
  componentIsActive = false;

  constructor(private readonly transactionService: AccountService) {}

  ngOnInit() {
    // this.transactionService.AccountCreateAccount({ email: 'evtolley@gmail.com', password: '1234567mmmm'})
    // .pipe(
    //   takeWhile(() => this.componentIsActive)
    // )
    // .subscribe(res => {
    //   console.log(res);
    // });
  }

  ngOnDestroy() {
    this.componentIsActive = false;
  }
}
