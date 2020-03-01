using Domain.LedgerTransactions;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Repositories
{
    public class LedgerTransactionRepository : ILedgerTransactionRepository
    {
        private readonly BankingLedgerContext _db;

        public LedgerTransactionRepository(BankingLedgerContext db)
        {
            this._db = db;
        }

        public LedgerTransactionDto GetLedgerTransaction(int? transactionId)
        {
            var transaction = this._db.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);

            if(transaction != null)
            {
                return transaction.GetLedgerTransactionDto();
            }

            return null;
        }

        public LedgerTransactionDto AddLedgerTransaction(LedgerTransactionDto transactionDto)
        {
            var account = _db.Accounts.SingleOrDefault(x => x.AccountId == transactionDto.AccountId);

            var transaction = new LedgerTransaction()
            {
                Amount = transactionDto.Amount,
                DateTimeCreatedUTC = transactionDto.DateTimeCreatedUTC,
                TransactionType = transactionDto.TransactionType
            };

            account.Transactions.Add(transaction);
            account.UpdateBalance(transaction);

            _db.Entry(account).State = EntityState.Modified;
            _db.SaveChanges();

            transactionDto.TransactionId = transaction.TransactionId;

            return transactionDto;
        }

        public LedgerTransactionDto EditLedgerTransaction(LedgerTransactionDto transactionDto)
        {
            var transaction = _db.Transactions.FirstOrDefault(x => x.TransactionId == transactionDto.TransactionId);
            var originalAmount = transaction.Amount;
            var originalTransactionType = transaction.TransactionType;

            var account = _db.Accounts.SingleOrDefault(x => x.AccountId == transactionDto.AccountId);

            transaction.Amount = transactionDto.Amount;
            transaction.TransactionType = transactionDto.TransactionType;
            _db.Entry(transaction).State = EntityState.Modified;

            account.RemoveTransactionAmountFromBalance(originalAmount, originalTransactionType);
            account.UpdateBalance(transaction);

            _db.Entry(account).State = EntityState.Modified;
            _db.SaveChanges();

            return transactionDto;
        }

        public IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId, int skip, int pageSize)
        {
            return _db.Transactions.Where(x => x.AccountId == accountId)
                .OrderByDescending(x => x.DateTimeCreatedUTC)
                .Skip(skip)
                .Take(pageSize)
                .Select(x => x.GetLedgerTransactionDto())
                .ToList();
        }

        public decimal GetCurrentBalance(int accountId)
        {
            var account = _db.Accounts.SingleOrDefault(x => x.AccountId == accountId);

            if (account != null)
            {
                return account.Balance;
            }

            throw new Exception($@"No account found for the provided accountId: {accountId}");
        }
    }
}
