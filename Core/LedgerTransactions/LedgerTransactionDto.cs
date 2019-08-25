using System;
using System.ComponentModel.DataAnnotations;

namespace Core.LedgerTransactions
{
    public class LedgerTransactionDto
    {
        [Required]
        public int AccountId { get; set; }
        public int TransactionId { get; set; }

        public decimal Amount { get; set; }
        public DateTime DateTimeCreatedUTC { get; set; }

        public LedgerTransactionTypeEnum TransactionType { get; set; }
    }
}
