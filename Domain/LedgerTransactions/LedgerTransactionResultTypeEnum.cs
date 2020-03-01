using System.ComponentModel;

namespace Domain.LedgerTransactions
{
    public enum LedgerTransactionResultTypeEnum
    {
        Success,

        [Description("This account does not have enough funds to process the requested transaction")]
        InsufficientFunds,
        [Description("Only transactions between zero and one million dollars are supported")]
        AmountOutOfRange,
        [Description("Invalid TransactionId provided")]
        InvalidTransactionId
    }
}
