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

        public LedgerTransactionResultDto MakeWithdrawal(InputLedgerTransactionDto transactionDto, int accountId)
        {
            // check to make sure the user has enough in account for this transaction
            if(transactionDto.Amount > _transactionRepo.GetCurrentBalance(accountId))
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.InsufficientFunds,
                    TransactionData = null
                };
            }

            return GenerateTransaction(transactionDto, accountId);
        }

        public LedgerTransactionResultDto MakeDeposit(InputLedgerTransactionDto transactionDto, int accountId)
        {
            return GenerateTransaction(transactionDto, accountId);
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

        private LedgerTransactionResultDto GenerateTransaction(InputLedgerTransactionDto transactionDto, int accountId)
        {
            if(transactionDto.Amount < 0 || transactionDto.Amount > 1000000)
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.AmountOutOfRange,
                    TransactionData = null
                };
            }

            var transaction = new LedgerTransactionDto()
            {
                AccountId = accountId,
                //rounding to nearest cent
                Amount = Math.Round(transactionDto.Amount, 2),
                TransactionType = transactionDto.TransactionType,
                DateTimeCreatedUTC = DateTime.UtcNow
            };


            return new LedgerTransactionResultDto()
            {
                ResultType = LedgerTransactionResultTypeEnum.Success,
                TransactionData = this._transactionRepo.AddLedgerTransaction(transaction)
            };
        }
    }
}
