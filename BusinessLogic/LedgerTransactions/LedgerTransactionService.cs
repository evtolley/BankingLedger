using Core.LedgerTransactions;
using Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace BusinessLogic.LedgerTransactions
{
    public class LedgerTransactionService : ILedgerTransactionService
    {
        private readonly ILedgerTransactionRepository _transactionRepo;
        public LedgerTransactionService(ILedgerTransactionRepository transactionRepo)
        {
            this._transactionRepo = transactionRepo;
        }

        public LedgerTransactionDto MakeWithdrawal(LedgerTransactionDto transactionDto)
        {
            transactionDto.Amount = Math.Round(transactionDto.Amount);
            transactionDto.TransactionType = LedgerTransactionTypeEnum.Withdrawal;
            transactionDto.DateTimeCreatedUTC = DateTime.UtcNow;
            return this._transactionRepo.AddLedgerTransaction(transactionDto);
        }

        public LedgerTransactionDto MakeDeposit(LedgerTransactionDto transactionDto)
        {
            transactionDto.Amount = Math.Round(transactionDto.Amount);
            transactionDto.TransactionType = LedgerTransactionTypeEnum.Deposit;
            transactionDto.DateTimeCreatedUTC = DateTime.UtcNow;
            return this._transactionRepo.AddLedgerTransaction(transactionDto);
        }

        public IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId)
        {
            return this._transactionRepo.GetAccountTransactions(accountId);
        }

        public decimal GetCurrentBalance(int accountId)
        {
            return this._transactionRepo.GetCurrentBalance(accountId);
        }
    }
}
