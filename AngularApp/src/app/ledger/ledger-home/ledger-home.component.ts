import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'ledger-home',
  templateUrl: './ledger-home.component.html',
  styleUrls: ['./ledger-home.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LedgerHomeComponent implements OnInit {

  constructor(private readonly toastr: ToastrService) { }

  ngOnInit() {
    const userEmail = localStorage.getItem('email');

    if (userEmail) {
      this.toastr.success(`Welcome ${userEmail}!`)
    }
  }

}
