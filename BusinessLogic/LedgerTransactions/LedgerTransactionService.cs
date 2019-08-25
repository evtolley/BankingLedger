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

        public LedgerTransactionResultDto MakeWithdrawal(LedgerTransactionDto transactionDto)
        {
            // check to make sure the user has enough in account for this transaction
            if(transactionDto.Amount > _transactionRepo.GetCurrentBalance(transactionDto.AccountId))
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.InsufficientFunds,
                    TransactionData = null
                };
            }

            transactionDto.Amount = Math.Round(transactionDto.Amount);
            transactionDto.TransactionType = LedgerTransactionTypeEnum.Withdrawal;
            transactionDto.DateTimeCreatedUTC = DateTime.UtcNow;

            return new LedgerTransactionResultDto()
            {
                ResultType = LedgerTransactionResultTypeEnum.Success,
                TransactionData = this._transactionRepo.AddLedgerTransaction(transactionDto)
            };
        }

        public LedgerTransactionResultDto MakeDeposit(LedgerTransactionDto transactionDto)
        {
            transactionDto.Amount = Math.Round(transactionDto.Amount);
            transactionDto.TransactionType = LedgerTransactionTypeEnum.Deposit;
            transactionDto.DateTimeCreatedUTC = DateTime.UtcNow;

            return new LedgerTransactionResultDto()
            {
                ResultType = LedgerTransactionResultTypeEnum.Success,
                TransactionData = this._transactionRepo.AddLedgerTransaction(transactionDto)
            };
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
