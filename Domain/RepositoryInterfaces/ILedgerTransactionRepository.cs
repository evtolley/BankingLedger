using Domain.LedgerTransactions;
using System.Collections.Generic;

namespace Domain.RepositoryInterfaces
{
    public interface ILedgerTransactionRepository
    {
        IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId, int skip, int pageSize);
        LedgerTransactionDto AddLedgerTransaction(LedgerTransactionDto transactionDto);
        decimal GetCurrentBalance(int accountId);
    }
}
