/* tslint:disable */
import { LedgerTransactionTypeEnum } from './ledger-transaction-type-enum';
export interface InputLedgerTransactionDto {
  amount: number;
  transactionType: LedgerTransactionTypeEnum;
}
