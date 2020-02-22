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

    getBalance(): Observable<void> {
        return this.transactionApiProxy.LedgerTransactionBalanceInquiry().pipe(
            map(res => {
                this.accountBalance$.next(res);
            })
        );
    }
}
