namespace Domain.LedgerTransactions
{
    public class LedgerTransactionResultDto
    {
        public LedgerTransactionResultTypeEnum ResultType { get; set; }
        public LedgerTransactionDto TransactionData { get; set; }

        public decimal AccountBalance { get; set; }
    }
}
