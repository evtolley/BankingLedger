using Core;
using Persistence.Entities;
using Persistence.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Transactions
{
    public class LedgerTransactionService : ILedgerTransactionService
    {
        private readonly ILedgerTransactionRepository _transactionRepo;
        public LedgerTransactionService(ILedgerTransactionRepository transactionRepo)
        {
            this._transactionRepo = transactionRepo;
        }

        public void AddLedgerTransaction(LedgerTransactionDto transactionDto)
        {
            this._transactionRepo.AddLedgerTransaction(new LedgerTransaction()
            {
                Amount = transactionDto.Amount,
                DateTimeCreatedUTC = DateTime.UtcNow
            });
        }

        public IEnumerable<LedgerTransactionDto> GetLedgerTransactions()
        {
            var transactions = this._transactionRepo.GetLedgerTransactions();

            var dtos = new HashSet<LedgerTransactionDto>();
            foreach(var transaction in transactions)
            {
                dtos.Add(new LedgerTransactionDto()
                {
                    Amount = transaction.Amount,
                    DateTimeCreatedUTC = transaction.DateTimeCreatedUTC,
                });
            }

            return dtos;
        }
    }
}
