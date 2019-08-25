using System.ComponentModel.DataAnnotations;

namespace Core.LedgerTransactions
{
    public class InputLedgerTransactionDto
    {
        [Range(0.01, 1000000)]
        public decimal Amount { get; set; }
    }
}
