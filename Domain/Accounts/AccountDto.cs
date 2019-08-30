namespace Domain.Accounts
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
