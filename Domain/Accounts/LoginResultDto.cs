namespace Domain.Accounts
{
    public class LoginResultDto
    {
        public LoginResultTypeEnum ResultType { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
