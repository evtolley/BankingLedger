using System.ComponentModel;

namespace Core.LedgerTransactions
{
    public enum LedgerTransactionResultTypeEnum
    {
        Success,

        [Description("There are insufficient funds in this account for the requested transaction")]
        InsufficientFunds,
        [Description("Only transactions between zero and one million dollars are supported")]
        AmountOutOfRange
    }
}
