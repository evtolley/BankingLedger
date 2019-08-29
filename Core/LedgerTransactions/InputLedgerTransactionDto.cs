using System.ComponentModel.DataAnnotations;

namespace Core.LedgerTransactions
{
    public class InputLedgerTransactionDto
    {
        public decimal Amount { get; set; }

        [Required]
        public LedgerTransactionTypeEnum TransactionType { get; set; }
    }
}
