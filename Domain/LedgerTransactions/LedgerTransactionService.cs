using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace Domain.LedgerTransactions
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
            if(!UserHasEnoughForNewWithdrawal(transactionDto.Amount, accountId))
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.InsufficientFunds,
                    TransactionData = null
                };
            }

            return GenerateNewTransaction(transactionDto, accountId);
        }

        public LedgerTransactionResultDto MakeDeposit(InputLedgerTransactionDto transactionDto, int accountId)
        {
            return GenerateNewTransaction(transactionDto, accountId);
        }
        public LedgerTransactionResultDto EditTransaction(InputLedgerTransactionDto transactionDto, int accountId)
        {
            var originalTransaction = _transactionRepo.GetLedgerTransaction(transactionDto.TransactionId);

            if(originalTransaction == null)
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.InvalidTransactionId,
                    TransactionData = null
                };
            }

            if(transactionDto.TransactionType == LedgerTransactionTypeEnum.Withdrawal 
            && !UserHasEnoughForUpdateWithdrawal(transactionDto.Amount, originalTransaction, accountId))
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.InsufficientFunds,
                    TransactionData = null
                };
            }

            var transactionResult = this._transactionRepo.EditLedgerTransaction(new LedgerTransactionDto()
            {
                AccountId = accountId,
                //if we get here, we can be sure that TransactionId has a value
                TransactionId = transactionDto.TransactionId.Value,
                //rounding to nearest cent
                Amount = Math.Round(transactionDto.Amount, 2),
                TransactionType = transactionDto.TransactionType
            });

            return new LedgerTransactionResultDto()
            {
                ResultType = LedgerTransactionResultTypeEnum.Success,
                TransactionData = transactionResult,
                AccountBalance = GetCurrentBalance(accountId)
            };
        }

        public IEnumerable<LedgerTransactionDto> GetAccountTransactions(int accountId, int skip, int pageSize)
        {
            return this._transactionRepo.GetAccountTransactions(accountId, skip, pageSize);
        }
        

        public decimal GetCurrentBalance(int accountId)
        {
            return this._transactionRepo.GetCurrentBalance(accountId);
        }

        private LedgerTransactionResultDto GenerateNewTransaction(InputLedgerTransactionDto transactionDto, int accountId)
        {
            if(transactionDto.Amount < 0.01m || transactionDto.Amount > 1000000)
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.AmountOutOfRange,
                    TransactionData = null
                };
            }

            var transactionResult = this._transactionRepo.AddLedgerTransaction(new LedgerTransactionDto()
            {
                AccountId = accountId,
                //rounding to nearest cent
                Amount = Math.Round(transactionDto.Amount, 2),
                TransactionType = transactionDto.TransactionType,
                DateTimeCreatedUTC = DateTime.UtcNow
            });

            return new LedgerTransactionResultDto()
            {
                ResultType = LedgerTransactionResultTypeEnum.Success,
                TransactionData = transactionResult,
                AccountBalance = GetCurrentBalance(accountId)
            };
        }

        private bool UserHasEnoughForNewWithdrawal(decimal amount, int accountId) 
        {
            return amount < _transactionRepo.GetCurrentBalance(accountId);
        }

        
        private bool UserHasEnoughForUpdateWithdrawal(
            decimal newAmount, 
            LedgerTransactionDto originalTransaction,
            int accountId) 
        {
            var currentBalance = _transactionRepo.GetCurrentBalance(accountId);

            if(originalTransaction.TransactionType == LedgerTransactionTypeEnum.Deposit)
            {
                return (currentBalance - originalTransaction.Amount - newAmount) > 0;
            }
            else
            {
                return (currentBalance + originalTransaction.Amount - newAmount) > 0;
            }
        }
    }
}
