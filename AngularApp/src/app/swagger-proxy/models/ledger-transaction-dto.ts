/* tslint:disable */
import { LedgerTransactionTypeEnum } from './ledger-transaction-type-enum';
export interface LedgerTransactionDto {
  accountId: number;
  transactionId: number;
  amount: number;
  dateTimeCreatedUTC: string;
  transactionType: LedgerTransactionTypeEnum;
}
