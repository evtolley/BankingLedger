using System.Threading.Tasks;
using Domain.Accounts;

namespace Domain.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task AddAccountAsync(AccountDto loginAttemptDto);
        Task<AccountDto> GetAccountOrDefaultByEmailAsync(string email);
    }
}
