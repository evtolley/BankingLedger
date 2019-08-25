using Core.Accounts;

namespace BusinessLogic.Accounts
{
    public interface IAccountService
    {
        CreateAccountResultDto CreateAccount(CreateAccountDto accountInfo);
        LoginResultDto Login(LoginAttemptDto loginInfo);
        void Logout();
    }
}
