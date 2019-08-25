using System.ComponentModel.DataAnnotations;

namespace Core.Accounts
{
    public class LoginAttemptDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
