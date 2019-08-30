using System.ComponentModel;

namespace Domain.Accounts
{
    public enum AccountCreationResultTypeEnum
    {
        [Description("Account created successfully")]
        Success,
        [Description("This account already exists")]
        AlreadyExists,
        [Description("The entered password does not meet all requirements. The password must be at least eight characters long and have at least one number and one letter.")]
        PasswordNotCompliant,
        [Description("The passwords do not match")]
        PasswordsDoNotMatch
    }
}
