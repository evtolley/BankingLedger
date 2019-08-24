using Persistence.Entities;
using Persistence.RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Repositories
{
    public class LedgerTransactionRepository : ILedgerTransactionRepository
    {
        internal BankingLedgerContext _db;

        public LedgerTransactionRepository(BankingLedgerContext db)
        {
            this._db = db;
        }

        public void AddLedgerTransaction(LedgerTransaction transaction)
        {
            _db.Transactions.Add(transaction);
            _db.SaveChanges();
        }

        public IEnumerable<LedgerTransaction> GetLedgerTransactions()
        {
            return _db.Transactions.ToList();
        }
    }
}
