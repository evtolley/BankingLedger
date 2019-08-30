namespace Domain.Accounts
{
    public class CreateAccountResultDto
    {
        public AccountCreationResultTypeEnum ResultType { get; set; }
        public LoginResultDto LoginData { get; set; }
    }
}
