import { Component, OnInit, Input } from '@angular/core';
import { LedgerTransactionDto } from 'src/app/swagger-proxy/models';

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

  update() {
    console.log('Save clicked');
    this.toggleEditMode();
  }
}
