using Core.Accounts;
using Persistence.Entities;
using Persistence.RepositoryInterfaces;
using System.Linq;

namespace Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        internal BankingLedgerContext _db;
        public AccountRepository(BankingLedgerContext db)
        {
            this._db = db;
        }

        public void AddAccount(AccountDto accountDto)
        {
            this._db.Accounts.Add(new Account()
            {
                Email = accountDto.Email,
                PasswordHash = accountDto.PasswordHash
            });
            _db.SaveChanges();
        }

        public AccountDto GetAccountOrDefaultByEmail(string email)
        {
            var account = _db.Accounts.FirstOrDefault(x => x.Email == email);

            if(account != null)
            {
                return new AccountDto()
                {
                    Email = account.Email,
                    PasswordHash = account.PasswordHash
                };
            }
            // if no account, return null
            return null;
        }
    }
}
