using Domain.Accounts;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingLedgerContext _db;
        public AccountRepository(BankingLedgerContext db)
        {
            this._db = db;
        }

        public async Task AddAccountAsync(AccountDto accountDto)
        {
            await this._db.Accounts.AddAsync(new Account()
            {
                Email = accountDto.Email,
                PasswordHash = accountDto.PasswordHash
            });

            await _db.SaveChangesAsync();
        }

        public async Task<AccountDto> GetAccountOrDefaultByEmailAsync(string email)
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(x => x.Email == email);

            if(account != null)
            {
                return new AccountDto()
                {
                    Email = account.Email,
                    AccountId = account.AccountId,
                    PasswordHash = account.PasswordHash
                };
            }
            // if no account, return null
            return null;
        }
    }
}
