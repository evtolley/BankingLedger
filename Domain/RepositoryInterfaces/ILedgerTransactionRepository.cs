using Domain.LedgerTransactions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface ILedgerTransactionRepository
    {
        Task<IEnumerable<LedgerTransactionDto>> GetAccountTransactionsAsync(int accountId, int skip, int pageSize);
        Task<LedgerTransactionDto> GetLedgerTransactionAsync(int? transactionId);
        Task<LedgerTransactionDto> AddLedgerTransactionAsync(LedgerTransactionDto transactionDto);
        Task<LedgerTransactionDto> EditLedgerTransactionAsync(LedgerTransactionDto transactionDto);
        Task DeleteLedgerTransactionAsync(int transactionId, int accountId);
        Task<decimal> GetCurrentBalanceAsync(int accountId);
    }
}
