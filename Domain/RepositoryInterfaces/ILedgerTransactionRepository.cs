using Domain.LedgerTransactions;
using System.Collections.Generic;

namespace Domain.RepositoryInterfaces
{
    public interface ILedgerTransactionRepository
    {
        IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId);
        LedgerTransactionDto AddLedgerTransaction(LedgerTransactionDto transactionDto);
        decimal GetCurrentBalance(int accountId);
    }
}
