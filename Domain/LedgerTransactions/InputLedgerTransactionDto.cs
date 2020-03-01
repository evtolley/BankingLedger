using System.ComponentModel.DataAnnotations;

namespace Domain.LedgerTransactions
{
    public class InputLedgerTransactionDto
    {
        public int? TransactionId { get; set;}
        public decimal Amount { get; set; }

        [Required]
        public LedgerTransactionTypeEnum TransactionType { get; set; }
    }
}
