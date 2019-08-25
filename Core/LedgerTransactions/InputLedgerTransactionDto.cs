using System.ComponentModel.DataAnnotations;

namespace Core.LedgerTransactions
{
    public class InputLedgerTransactionDto
    {
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
