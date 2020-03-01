using Domain.LedgerTransactions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    public class LedgerTransaction
    {
        [Key]
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTimeCreatedUTC { get; set; }
        public LedgerTransactionTypeEnum TransactionType { get; set; }

        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public LedgerTransactionDto GetLedgerTransactionDto()
        {
            return new LedgerTransactionDto()
            {
                Amount = this.Amount,
                DateTimeCreatedUTC = this.DateTimeCreatedUTC,
                AccountId = this.AccountId,
                TransactionId = this.TransactionId,
                TransactionType = this.TransactionType
            };
        }
    }
}
