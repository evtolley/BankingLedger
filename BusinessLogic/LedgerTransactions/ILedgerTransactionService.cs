using Core.LedgerTransactions;
using System.Collections.Generic;

namespace BusinessLogic.LedgerTransactions
{
    public interface ILedgerTransactionService
    {
        IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId);
        LedgerTransactionResultDto MakeWithdrawal(InputLedgerTransactionDto transactionDto, int accountId);
        LedgerTransactionResultDto MakeDeposit(InputLedgerTransactionDto transactionDto, int accountId);
        decimal GetCurrentBalance(int accountId);
    }
}
