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
        public string PasswordHash { get; set; }

        public ICollection<LedgerTransaction> Transactions { get; set; }
    }
}
