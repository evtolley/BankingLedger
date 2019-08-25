using System.ComponentModel;

namespace Core.Accounts
{
    public enum LoginResultTypeEnum
    {
        [Description("Login successful")]
        Success,
        [Description("Login failed")]
        Failure
    }
}
