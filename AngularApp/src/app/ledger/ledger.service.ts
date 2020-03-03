import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { LedgerTransactionService } from '../swagger-proxy/services';
import { LedgerTransactionDto, InputLedgerTransactionDto } from '../swagger-proxy/models';
import { map } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class LedgerService {
    constructor(private readonly transactionApiProxy: LedgerTransactionService, private readonly toastrService: ToastrService) {

    }
    transactions$ = new BehaviorSubject<LedgerTransactionDto[]>([]);
    accountBalance$ = new BehaviorSubject<number>(0);
    allTransactionsLoaded$ = new BehaviorSubject<boolean>(false);

    loadTransactions (pageSize: number): Observable<void> {
        return this.transactionApiProxy.LedgerTransactionGetTransactions({ Skip: this.transactions$.value.length, PageSize: pageSize }).pipe(
            map(res => {
                if(res.length < pageSize) {
                    this.allTransactionsLoaded$.next(true);
                }
                this.transactions$.next([...this.transactions$.value, ...res]);
            })
        );
    }

    addTransaction(transaction: InputLedgerTransactionDto): Observable<void> {
        return this.transactionApiProxy.LedgerTransactionCreate(transaction).pipe(
            map(res => {
                if (res.transactionData) {
                    this.transactions$.next([res.transactionData, ...this.transactions$.value]);
                    this.accountBalance$.next(res.accountBalance);
                }
            })
        );
    }

    editTransaction(transaction: InputLedgerTransactionDto): Observable<void> {
        return this.transactionApiProxy.LedgerTransactionEdit(transaction).pipe(
            map(res => {
                if (res.transactionData) {
                    const index = this.transactions$.value.findIndex(item => item.transactionId === res.transactionData.transactionId);
                    let transactions = this.transactions$.value;

                    transactions[index].amount = res.transactionData.amount;
                    transactions[index].transactionType = res.transactionData.transactionType;
                    this.transactions$.next([...transactions]);

                    this.accountBalance$.next(res.accountBalance);
                }
            })
        );
    }

    deleteTransaction(transaction: InputLedgerTransactionDto): Observable<void> {
        return this.transactionApiProxy.LedgerTransactionDelete(transaction.transactionId).pipe(
            map(res => {
                this.transactions$.next(this.transactions$.value.filter(item => item.transactionId !== transaction.transactionId));
                this.accountBalance$.next(res.accountBalance)
            })
        );
    }

    getBalance(): Observable<void> {
        return this.transactionApiProxy.LedgerTransactionBalanceInquiry().pipe(
            map(res => {
                this.accountBalance$.next(res);
            })
        );
    }
}
