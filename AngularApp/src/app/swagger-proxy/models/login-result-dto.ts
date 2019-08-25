/* tslint:disable */
import { LoginResultTypeEnum } from './login-result-type-enum';
export interface LoginResultDto {
  resultType: LoginResultTypeEnum;
  email?: string;
  token?: string;
}
