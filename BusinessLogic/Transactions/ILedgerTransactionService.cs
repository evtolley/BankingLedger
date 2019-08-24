using Core;
using Persistence.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Transactions
{
    public interface ILedgerTransactionService
    {
        IEnumerable<LedgerTransactionDto> GetLedgerTransactions();
        void AddLedgerTransaction(LedgerTransactionDto transactionDto);
    }
}
