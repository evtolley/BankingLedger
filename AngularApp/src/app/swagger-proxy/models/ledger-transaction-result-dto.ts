/* tslint:disable */
import { LedgerTransactionResultTypeEnum } from './ledger-transaction-result-type-enum';
import { LedgerTransactionDto } from './ledger-transaction-dto';
export interface LedgerTransactionResultDto {
  resultType: LedgerTransactionResultTypeEnum;
  transactionData?: LedgerTransactionDto;
}
