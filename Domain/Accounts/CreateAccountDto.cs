using System.ComponentModel.DataAnnotations;

namespace Domain.Accounts
{
    public class CreateAccountDto
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
