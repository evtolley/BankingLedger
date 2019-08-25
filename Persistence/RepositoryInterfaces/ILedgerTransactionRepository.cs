using Core.LedgerTransactions;
using System.Collections.Generic;

namespace Persistence.RepositoryInterfaces
{
    public interface ILedgerTransactionRepository
    {
        IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId);
        LedgerTransactionDto AddLedgerTransaction(LedgerTransactionDto transactionDto);
        decimal GetCurrentBalance(int accountId);
    }
}
