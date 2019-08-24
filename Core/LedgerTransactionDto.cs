using System;

namespace Core
{
    public class LedgerTransactionDto
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTimeCreatedUTC { get; set; }
    }
}
