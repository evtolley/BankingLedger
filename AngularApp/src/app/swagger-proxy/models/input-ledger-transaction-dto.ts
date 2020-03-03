/* tslint:disable */
import { LedgerTransactionTypeEnum } from './ledger-transaction-type-enum';
export interface InputLedgerTransactionDto {
  transactionId?: number;
  amount: number;
  transactionType: LedgerTransactionTypeEnum;
}
