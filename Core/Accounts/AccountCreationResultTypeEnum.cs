using System.ComponentModel;

namespace Core.Accounts
{
    public enum AccountCreationResultTypeEnum
    {
        [Description("Account created successfully")]
        Success,
        [Description("This account already exists")]
        AlreadyExists,
        [Description("The entered password does not meet all requirements. The password Must be at least eight characters long and have at least one number and one letter.")]
        PasswordNotCompliant
    }
}
