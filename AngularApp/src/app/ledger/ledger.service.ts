import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LedgerTransactionService } from '../swagger-proxy/services';
import { LedgerTransactionDto } from '../swagger-proxy/models';
import { map } from 'rxjs/operators';

@Injectable()
export class LedgerService {
    constructor(private readonly transactionApiProxy: LedgerTransactionService) {

    }
    transactions$ = new BehaviorSubject<LedgerTransactionDto[]>([]);

    loadTransactions (): Observable<void> {
        return this.transactionApiProxy.LedgerTransactionGetTransactions().pipe(
            map(res => {
                this.transactions$.next(res);
            })
        );
    }
}
