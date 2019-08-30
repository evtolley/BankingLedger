namespace Domain.Accounts
{
    public class JwtConfiguration
    {
        public string SecurityKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
    }
}
