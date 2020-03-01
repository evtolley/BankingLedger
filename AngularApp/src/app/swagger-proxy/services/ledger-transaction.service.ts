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
  static readonly LedgerTransactionCreatePath = '/api/LedgerTransaction/create';
  static readonly LedgerTransactionEditPath = '/api/LedgerTransaction/edit';
  static readonly LedgerTransactionBalanceInquiryPath = '/api/LedgerTransaction/balanceinquiry';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `LedgerTransactionService.LedgerTransactionGetTransactionsParams` containing the following parameters:
   *
   * - `Skip`:
   *
   * - `PageSize`:
   */
  LedgerTransactionGetTransactionsResponse(params: LedgerTransactionService.LedgerTransactionGetTransactionsParams): __Observable<__StrictHttpResponse<Array<LedgerTransactionDto>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.Skip != null) __params = __params.set('Skip', params.Skip.toString());
    if (params.PageSize != null) __params = __params.set('PageSize', params.PageSize.toString());
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
  }
  /**
   * @param params The `LedgerTransactionService.LedgerTransactionGetTransactionsParams` containing the following parameters:
   *
   * - `Skip`:
   *
   * - `PageSize`:
   */
  LedgerTransactionGetTransactions(params: LedgerTransactionService.LedgerTransactionGetTransactionsParams): __Observable<Array<LedgerTransactionDto>> {
    return this.LedgerTransactionGetTransactionsResponse(params).pipe(
      __map(_r => _r.body as Array<LedgerTransactionDto>)
    );
  }

  /**
   * @param ledgerTransactionDto undefined
   */
  LedgerTransactionCreateResponse(ledgerTransactionDto: InputLedgerTransactionDto): __Observable<__StrictHttpResponse<LedgerTransactionResultDto>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = ledgerTransactionDto;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/LedgerTransaction/create`,
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
  LedgerTransactionCreate(ledgerTransactionDto: InputLedgerTransactionDto): __Observable<LedgerTransactionResultDto> {
    return this.LedgerTransactionCreateResponse(ledgerTransactionDto).pipe(
      __map(_r => _r.body as LedgerTransactionResultDto)
    );
  }

  /**
   * @param ledgerTransactionDto undefined
   */
  LedgerTransactionEditResponse(ledgerTransactionDto: InputLedgerTransactionDto): __Observable<__StrictHttpResponse<LedgerTransactionResultDto>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = ledgerTransactionDto;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/LedgerTransaction/edit`,
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
  LedgerTransactionEdit(ledgerTransactionDto: InputLedgerTransactionDto): __Observable<LedgerTransactionResultDto> {
    return this.LedgerTransactionEditResponse(ledgerTransactionDto).pipe(
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

  /**
   * Parameters for LedgerTransactionGetTransactions
   */
  export interface LedgerTransactionGetTransactionsParams {
    Skip?: number;
    PageSize?: number;
  }
}

export { LedgerTransactionService }
