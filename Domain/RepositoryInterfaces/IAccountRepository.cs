using Domain.Accounts;

namespace Domain.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        void AddAccount(AccountDto loginAttemptDto);
        AccountDto GetAccountOrDefaultByEmail(string email);
    }
}
