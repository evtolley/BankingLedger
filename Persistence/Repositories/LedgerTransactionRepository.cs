﻿using Core.LedgerTransactions;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Repositories
{
    public class LedgerTransactionRepository : ILedgerTransactionRepository
    {
        internal BankingLedgerContext _db;

        public LedgerTransactionRepository(BankingLedgerContext db)
        {
            this._db = db;
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
            _db.Entry(account).State = EntityState.Modified;
            _db.SaveChanges();

            transactionDto.TransactionId = transaction.TransactionId;

            return transactionDto;
        }

        public IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId)
        {
            return _db.Transactions.Where(x => x.AccountId == accountId)
                .OrderByDescending(x => x.DateTimeCreatedUTC)
                .Select(x => new LedgerTransactionDto()
                {
                    Amount = x.Amount,
                    DateTimeCreatedUTC = x.DateTimeCreatedUTC,
                    AccountId = x.AccountId,
                    TransactionId = x.TransactionId,
                    TransactionType = x.TransactionType
                })
                .ToList();
        }

        public decimal GetCurrentBalance(int accountId)
        {
            return Math.Round(_db.Transactions
                .Where(x => x.AccountId == accountId && x.TransactionType == LedgerTransactionTypeEnum.Deposit)
                .Sum(x => x.Amount)
                - _db.Transactions
                .Where(x => x.AccountId == accountId && x.TransactionType == LedgerTransactionTypeEnum.Withdrawal)
                .Sum(x => x.Amount + -1), 2);
        }
    }
}
