using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Entities
{
    public class LedgerTransaction
    {
        [Key]
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTimeCreatedUTC { get; set; }

        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
