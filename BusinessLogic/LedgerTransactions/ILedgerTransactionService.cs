using Core.LedgerTransactions;
using System.Collections.Generic;

namespace BusinessLogic.LedgerTransactions
{
    public interface ILedgerTransactionService
    {
        IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId);
        LedgerTransactionResultDto MakeWithdrawal(LedgerTransactionDto transactionDto);
        LedgerTransactionResultDto MakeDeposit(LedgerTransactionDto transactionDto);
        decimal GetCurrentBalance(int accountId);
    }
}
