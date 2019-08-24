using Persistence.Entities;
using System.Collections.Generic;

namespace Persistence.RepositoryInterfaces
{
    public interface ILedgerTransactionRepository
    {
        IEnumerable<LedgerTransaction> GetLedgerTransactions();
        void AddLedgerTransaction(LedgerTransaction transaction);
    }
}
