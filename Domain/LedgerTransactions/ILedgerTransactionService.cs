using System.Collections.Generic;

namespace Domain.LedgerTransactions
{
    public interface ILedgerTransactionService
    {
        IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId, int skip, int pageSize);
        LedgerTransactionResultDto MakeWithdrawal(InputLedgerTransactionDto transactionDto, int accountId);
        LedgerTransactionResultDto MakeDeposit(InputLedgerTransactionDto transactionDto, int accountId);
        LedgerTransactionResultDto EditTransaction(InputLedgerTransactionDto transactionDto, int accountId);
        decimal GetCurrentBalance(int accountId);
    }
}
