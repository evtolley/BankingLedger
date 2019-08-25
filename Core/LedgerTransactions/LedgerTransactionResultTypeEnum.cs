using System.ComponentModel;

namespace Core.LedgerTransactions
{
    public enum LedgerTransactionResultTypeEnum
    {
        Success,

        [Description("There are insufficient funds in this account for the requested transaction")]
        InsufficientFunds
    }
}
