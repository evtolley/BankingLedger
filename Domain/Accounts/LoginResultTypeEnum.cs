using System.ComponentModel;

namespace Domain.Accounts
{
    public enum LoginResultTypeEnum
    {
        [Description("Login successful")]
        Success,
        [Description("Login failed")]
        Failure
    }
}
