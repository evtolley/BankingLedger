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

    loadTransactions (): Observable<void> {
        return this.transactionApiProxy.LedgerTransactionGetTransactions().pipe(
            map(res => {
                this.transactions$.next(res);
            })
        );
    }

    addTransaction(transaction: InputLedgerTransactionDto): Observable<void> {
        return this.transactionApiProxy.LedgerTransactionCreate(transaction).pipe(
            map(res => {
                if (res.transactionData) {
                    this.transactions$.next([res.transactionData, ...this.transactions$.value]);
                }
            })
        );
    }
}
