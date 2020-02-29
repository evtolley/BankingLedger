import { Component, OnInit, Input } from '@angular/core';
import { LedgerTransactionDto, InputLedgerTransactionDto } from 'src/app/swagger-proxy/models';

@Component({
  selector: 'transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {

  constructor() { }

  editMode = false;

  @Input()
  transaction: LedgerTransactionDto;

  ngOnInit() {
  }

  toggleEditMode() {
    this.editMode = !this.editMode;
  }

  update(model: InputLedgerTransactionDto) {
    console.log(model);
    this.toggleEditMode();
  }
}
