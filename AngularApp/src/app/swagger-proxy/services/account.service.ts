/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { CreateAccountResultDto } from '../models/create-account-result-dto';
import { CreateAccountDto } from '../models/create-account-dto';
import { LoginResultDto } from '../models/login-result-dto';
import { LoginAttemptDto } from '../models/login-attempt-dto';
@Injectable({
  providedIn: 'root',
})
class AccountService extends __BaseService {
  static readonly AccountCreateAccountPath = '/api/Account/create';
  static readonly AccountLoginPath = '/api/Account/login';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param accountDto undefined
   */
  AccountCreateAccountResponse(accountDto: CreateAccountDto): __Observable<__StrictHttpResponse<CreateAccountResultDto>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = accountDto;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Account/create`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<CreateAccountResultDto>;
      })
    );
  }
  /**
   * @param accountDto undefined
   */
  AccountCreateAccount(accountDto: CreateAccountDto): __Observable<CreateAccountResultDto> {
    return this.AccountCreateAccountResponse(accountDto).pipe(
      __map(_r => _r.body as CreateAccountResultDto)
    );
  }

  /**
   * @param loginInfo undefined
   */
  AccountLoginResponse(loginInfo: LoginAttemptDto): __Observable<__StrictHttpResponse<LoginResultDto>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = loginInfo;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Account/login`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<LoginResultDto>;
      })
    );
  }
  /**
   * @param loginInfo undefined
   */
  AccountLogin(loginInfo: LoginAttemptDto): __Observable<LoginResultDto> {
    return this.AccountLoginResponse(loginInfo).pipe(
      __map(_r => _r.body as LoginResultDto)
    );
  }
}

module AccountService {
}

export { AccountService }
