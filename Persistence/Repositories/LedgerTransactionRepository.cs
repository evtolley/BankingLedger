using Domain.LedgerTransactions;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class LedgerTransactionRepository : ILedgerTransactionRepository
    {
        private readonly BankingLedgerContext _db;

        public LedgerTransactionRepository(BankingLedgerContext db)
        {
            this._db = db;
        }

        public async Task<LedgerTransactionDto> GetLedgerTransactionAsync(int? transactionId)
        {
            var transaction = await this._db.Transactions.FirstOrDefaultAsync(x => x.TransactionId == transactionId);

            if(transaction != null)
            {
                return transaction.GetLedgerTransactionDto();
            }

            return null;
        }

        public async Task<LedgerTransactionDto> AddLedgerTransactionAsync(LedgerTransactionDto transactionDto)
        {
            var account = await _db.Accounts.SingleOrDefaultAsync(x => x.AccountId == transactionDto.AccountId);

            var transaction = new LedgerTransaction()
            {
                Amount = transactionDto.Amount,
                DateTimeCreatedUTC = transactionDto.DateTimeCreatedUTC,
                TransactionType = transactionDto.TransactionType
            };

            account.Transactions.Add(transaction);
            account.UpdateBalance(transaction);

            _db.Entry(account).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            transactionDto.TransactionId = transaction.TransactionId;

            return transactionDto;
        }

        public async Task<LedgerTransactionDto> EditLedgerTransactionAsync(LedgerTransactionDto transactionDto)
        {
            var transaction = await _db.Transactions.FirstOrDefaultAsync(x => x.TransactionId == transactionDto.TransactionId);
            var originalAmount = transaction.Amount;
            var originalTransactionType = transaction.TransactionType;

            var account = await _db.Accounts.SingleOrDefaultAsync(x => x.AccountId == transactionDto.AccountId);

            transaction.Amount = transactionDto.Amount;
            transaction.TransactionType = transactionDto.TransactionType;
            _db.Entry(transaction).State = EntityState.Modified;

            account.RemoveTransactionAmountFromBalance(originalAmount, originalTransactionType);
            account.UpdateBalance(transaction);

            _db.Entry(account).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return transactionDto;
        }

        public async Task<IEnumerable<LedgerTransactionDto>> GetAccountTransactionsAsync(int accountId, int skip, int pageSize)
        {
            return await _db.Transactions.Where(x => x.AccountId == accountId)
                .OrderByDescending(x => x.DateTimeCreatedUTC)
                .Skip(skip)
                .Take(pageSize)
                .Select(x => x.GetLedgerTransactionDto())
                .ToListAsync();
        }

        public async Task DeleteLedgerTransactionAsync(int transactionId, int accountId)
        {
           var account = await _db.Accounts.SingleOrDefaultAsync(x => x.AccountId == accountId);

            var transaction = await _db.Transactions.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            this._db.Transactions.Remove(transaction);

            account.UpdateBalanceWhenTransactionDeleted(transaction);


            await _db.SaveChangesAsync();
        }


        public async Task<decimal> GetCurrentBalanceAsync(int accountId)
        {
            var account = await _db.Accounts.SingleOrDefaultAsync(x => x.AccountId == accountId);

            if (account != null)
            {
                return account.Balance;
            }

            throw new Exception($@"No account found for the provided accountId: {accountId}");
        }
    }
}
