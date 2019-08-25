using Core.Accounts;

namespace Persistence.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        void AddAccount(AccountDto loginAttemptDto);
        AccountDto GetAccountOrDefaultByEmail(string email);
    }
}
