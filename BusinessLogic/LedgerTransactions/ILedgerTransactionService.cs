using Core.LedgerTransactions;
using System.Collections.Generic;

namespace BusinessLogic.LedgerTransactions
{
    public interface ILedgerTransactionService
    {
        IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId);
        LedgerTransactionDto MakeWithdrawal(LedgerTransactionDto transactionDto);
        LedgerTransactionDto MakeDeposit(LedgerTransactionDto transactionDto);
        decimal GetCurrentBalance(int accountId);
    }
}
