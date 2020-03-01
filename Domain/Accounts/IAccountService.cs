using System.Threading.Tasks;

namespace Domain.Accounts
{
    public interface IAccountService
    {
        Task <CreateAccountResultDto> CreateAccountAsync(CreateAccountDto accountInfo);
        Task<LoginResultDto> LoginAsync(LoginAttemptDto loginInfo);
    }
}
