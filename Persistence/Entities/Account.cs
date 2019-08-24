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
        public string Email { get; set; }

        //should be hashing!!!
        public string Password { get; set; }

        public ICollection<LedgerTransaction> Transactions { get; set; }
    }
}
