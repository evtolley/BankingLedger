using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.LedgerTransactions;

namespace Persistence.Entities
{
    public class Account
    {
        public Account()
        {
            this.Transactions = new HashSet<LedgerTransaction>();
        }

        [Key]
        public int AccountId { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public decimal Balance { get; set; }

        public ICollection<LedgerTransaction> Transactions { get; set; }

        public void UpdateBalance(LedgerTransaction transaction)
        {
            if(transaction.TransactionType == LedgerTransactionTypeEnum.Deposit)
            {
                this.Balance += transaction.Amount;
            }
            else
            {
                this.Balance -= transaction.Amount;
            }
        }

        public void RemoveTransactionAmountFromBalance(decimal amount, LedgerTransactionTypeEnum transactionType)
        {
            if(transactionType == LedgerTransactionTypeEnum.Deposit)
            {
                this.Balance -= amount;
            }
            else
            {
                this.Balance += amount;
            }
        }

        public void UpdateBalanceWhenTransactionDeleted(LedgerTransaction transaction)
        {
            this.Balance -= transaction.Amount;
        }
    }
}
