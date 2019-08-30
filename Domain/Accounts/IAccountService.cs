namespace Domain.Accounts
{
    public interface IAccountService
    {
        CreateAccountResultDto CreateAccount(CreateAccountDto accountInfo);
        LoginResultDto Login(LoginAttemptDto loginInfo);
        void Logout();
    }
}
