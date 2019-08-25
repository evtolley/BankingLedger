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

            return GenerateTransaction(transactionDto, LedgerTransactionTypeEnum.Withdrawal);
        }

        public LedgerTransactionResultDto MakeDeposit(LedgerTransactionDto transactionDto)
        {
            return GenerateTransaction(transactionDto, LedgerTransactionTypeEnum.Deposit);
        }

        // should have paging functionality. Skipping for brevity of this code sample
        public IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId)
        {
            return this._transactionRepo.GetAccountTransactions(accountId);
        }

        public decimal GetCurrentBalance(int accountId)
        {
            return this._transactionRepo.GetCurrentBalance(accountId);
        }

        private LedgerTransactionResultDto GenerateTransaction(LedgerTransactionDto transactionDto, LedgerTransactionTypeEnum transactionType)
        {
            // rounding entered amount to nearest cent
            transactionDto.Amount = Math.Round(transactionDto.Amount, 2);
            transactionDto.TransactionType = transactionType;
            transactionDto.DateTimeCreatedUTC = DateTime.UtcNow;

            return new LedgerTransactionResultDto()
            {
                ResultType = LedgerTransactionResultTypeEnum.Success,
                TransactionData = this._transactionRepo.AddLedgerTransaction(transactionDto)
            };
        }
    }
}
