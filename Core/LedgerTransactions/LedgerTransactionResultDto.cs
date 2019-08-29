using System;
using System.Collections.Generic;
using System.Text;

namespace Core.LedgerTransactions
{
    public class LedgerTransactionResultDto
    {
        public LedgerTransactionResultTypeEnum ResultType { get; set; }
        public LedgerTransactionDto TransactionData { get; set; }

        public decimal AccountBalance { get; set; }
    }
}
