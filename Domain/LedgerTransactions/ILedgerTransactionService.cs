using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.LedgerTransactions
{
    public interface ILedgerTransactionService
    {
        Task<IEnumerable<LedgerTransactionDto>> GetAccountTransactionsAsync(int accountId, int skip, int pageSize);
        Task<LedgerTransactionResultDto> MakeWithdrawalAsync(InputLedgerTransactionDto transactionDto, int accountId);
        Task<LedgerTransactionResultDto> MakeDepositAsync(InputLedgerTransactionDto transactionDto, int accountId);
        Task<LedgerTransactionResultDto> EditTransactionAsync(InputLedgerTransactionDto transactionDto, int accountId);
        Task<LedgerTransactionResultDto> DeleteTransactionAsync(int transactionId, int accountId);
        Task<decimal> GetCurrentBalanceAsync(int accountId);
    }
}
