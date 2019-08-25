/* tslint:disable */
import { AccountCreationResultTypeEnum } from './account-creation-result-type-enum';
import { LoginResultDto } from './login-result-dto';
export interface CreateAccountResultDto {
  resultType: AccountCreationResultTypeEnum;
  loginData?: LoginResultDto;
}
