using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities
{
    public class Account
    {
        public Account()
        {
            this.Transactions = new HashSet<LedgerTransaction>();
        }

        [Key]
        public int AccountId { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public int LoginAttempts { get; set; } = 0;
        public string PasswordHash { get; set; }

        public ICollection<LedgerTransaction> Transactions { get; set; }
    }
}
