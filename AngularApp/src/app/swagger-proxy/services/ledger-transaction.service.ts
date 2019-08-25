/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { LedgerTransactionDto } from '../models/ledger-transaction-dto';
import { LedgerTransactionResultDto } from '../models/ledger-transaction-result-dto';
import { InputLedgerTransactionDto } from '../models/input-ledger-transaction-dto';
@Injectable({
  providedIn: 'root',
})
class LedgerTransactionService extends __BaseService {
  static readonly LedgerTransactionGetTransactionsPath = '/api/LedgerTransaction/gettransactions';
  static readonly LedgerTransactionWithdrawalPath = '/api/LedgerTransaction/withdrawal';
  static readonly LedgerTransactionDepositPath = '/api/LedgerTransaction/deposit';
  static readonly LedgerTransactionBalanceInquiryPath = '/api/LedgerTransaction/balanceinquiry';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }
  LedgerTransactionGetTransactionsResponse(): __Observable<__StrictHttpResponse<Array<LedgerTransactionDto>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/LedgerTransaction/gettransactions`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<LedgerTransactionDto>>;
      })
    );
  }  LedgerTransactionGetTransactions(): __Observable<Array<LedgerTransactionDto>> {
    return this.LedgerTransactionGetTransactionsResponse().pipe(
      __map(_r => _r.body as Array<LedgerTransactionDto>)
    );
  }

  /**
   * @param ledgerTransactionDto undefined
   */
  LedgerTransactionWithdrawalResponse(ledgerTransactionDto: InputLedgerTransactionDto): __Observable<__StrictHttpResponse<LedgerTransactionResultDto>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = ledgerTransactionDto;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/LedgerTransaction/withdrawal`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<LedgerTransactionResultDto>;
      })
    );
  }
  /**
   * @param ledgerTransactionDto undefined
   */
  LedgerTransactionWithdrawal(ledgerTransactionDto: InputLedgerTransactionDto): __Observable<LedgerTransactionResultDto> {
    return this.LedgerTransactionWithdrawalResponse(ledgerTransactionDto).pipe(
      __map(_r => _r.body as LedgerTransactionResultDto)
    );
  }

  /**
   * @param ledgerTransactionDto undefined
   */
  LedgerTransactionDepositResponse(ledgerTransactionDto: InputLedgerTransactionDto): __Observable<__StrictHttpResponse<LedgerTransactionResultDto>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = ledgerTransactionDto;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/LedgerTransaction/deposit`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<LedgerTransactionResultDto>;
      })
    );
  }
  /**
   * @param ledgerTransactionDto undefined
   */
  LedgerTransactionDeposit(ledgerTransactionDto: InputLedgerTransactionDto): __Observable<LedgerTransactionResultDto> {
    return this.LedgerTransactionDepositResponse(ledgerTransactionDto).pipe(
      __map(_r => _r.body as LedgerTransactionResultDto)
    );
  }
  LedgerTransactionBalanceInquiryResponse(): __Observable<__StrictHttpResponse<number>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/LedgerTransaction/balanceinquiry`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'text'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return (_r as HttpResponse<any>).clone({ body: parseFloat((_r as HttpResponse<any>).body as string) }) as __StrictHttpResponse<number>
      })
    );
  }  LedgerTransactionBalanceInquiry(): __Observable<number> {
    return this.LedgerTransactionBalanceInquiryResponse().pipe(
      __map(_r => _r.body as number)
    );
  }
}

module LedgerTransactionService {
}

export { LedgerTransactionService }
