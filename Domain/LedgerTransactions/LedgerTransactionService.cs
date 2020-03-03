using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.LedgerTransactions
{
    public class LedgerTransactionService : ILedgerTransactionService
    {
        private readonly ILedgerTransactionRepository _transactionRepo;
        public LedgerTransactionService(ILedgerTransactionRepository transactionRepo)
        {
            this._transactionRepo = transactionRepo;
        }

        public async Task<LedgerTransactionResultDto> MakeWithdrawalAsync(InputLedgerTransactionDto transactionDto, int accountId)
        {
            // check to make sure the user has enough in account for this transaction
            if(!await UserHasEnoughForNewWithdrawalAsync(transactionDto.Amount, accountId))
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.InsufficientFunds,
                    TransactionData = null
                };
            }

            return await GenerateNewTransactionAsync(transactionDto, accountId);
        }

        public async Task<LedgerTransactionResultDto> MakeDepositAsync(InputLedgerTransactionDto transactionDto, int accountId)
        {
            return await GenerateNewTransactionAsync(transactionDto, accountId);
        }
        public async Task<LedgerTransactionResultDto> EditTransactionAsync(InputLedgerTransactionDto transactionDto, int accountId)
        {
            var originalTransaction = await _transactionRepo.GetLedgerTransactionAsync(transactionDto.TransactionId);

            if(originalTransaction == null)
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.InvalidTransactionId,
                    TransactionData = null
                };
            }

            if(originalTransaction.AccountId != accountId)
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.PermissionError,
                    TransactionData = null
                };
            }

            if(transactionDto.TransactionType == LedgerTransactionTypeEnum.Withdrawal 
            && !await UserHasEnoughForUpdateWithdrawalAsync(transactionDto.Amount, originalTransaction, accountId))
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.InsufficientFunds,
                    TransactionData = null
                };
            }

            var transactionResult = await this._transactionRepo.EditLedgerTransactionAsync(new LedgerTransactionDto()
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
                AccountBalance = await GetCurrentBalanceAsync(accountId)
            };
        }
        public async Task<LedgerTransactionResultDto> DeleteTransactionAsync(int transactionId, int accountId)
        {
            var originalTransaction = await _transactionRepo.GetLedgerTransactionAsync(transactionId);

            if(originalTransaction == null)
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.InvalidTransactionId,
                    TransactionData = null
                };
            }

            if(originalTransaction.AccountId != accountId)
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.PermissionError,
                    TransactionData = null
                };
            }
            await this._transactionRepo.DeleteLedgerTransactionAsync(transactionId, accountId);

            return new LedgerTransactionResultDto()
            {
                ResultType = LedgerTransactionResultTypeEnum.Success,
                TransactionData = null,
                AccountBalance = await GetCurrentBalanceAsync(accountId)
            };
        }

        public async Task<IEnumerable<LedgerTransactionDto>> GetAccountTransactionsAsync(int accountId, int skip, int pageSize)
        {
            return await this._transactionRepo.GetAccountTransactionsAsync(accountId, skip, pageSize);
        }
        
        public async Task<decimal> GetCurrentBalanceAsync(int accountId)
        {
            return await this._transactionRepo.GetCurrentBalanceAsync(accountId);
        }

        private async Task<LedgerTransactionResultDto> GenerateNewTransactionAsync(InputLedgerTransactionDto transactionDto, int accountId)
        {
            if(transactionDto.Amount < 0.01m || transactionDto.Amount > 1000000)
            {
                return new LedgerTransactionResultDto()
                {
                    ResultType = LedgerTransactionResultTypeEnum.AmountOutOfRange,
                    TransactionData = null
                };
            }

            var transactionResult = await this._transactionRepo.AddLedgerTransactionAsync(new LedgerTransactionDto()
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
                AccountBalance = await GetCurrentBalanceAsync(accountId)
            };
        }

        private async Task<bool> UserHasEnoughForNewWithdrawalAsync(decimal amount, int accountId) 
        {
            var currentBalance = await GetCurrentBalanceAsync(accountId);
            return amount < currentBalance;
        }
        
        private async Task<bool> UserHasEnoughForUpdateWithdrawalAsync(
            decimal newAmount, 
            LedgerTransactionDto originalTransaction,
            int accountId) 
        {
            var currentBalance = await GetCurrentBalanceAsync(accountId);

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
