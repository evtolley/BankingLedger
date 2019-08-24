using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence
{
    public class BankingLedgerContext : DbContext
    {
        public BankingLedgerContext(DbContextOptions<BankingLedgerContext> options)
           : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<LedgerTransaction> Transactions { get; set; }
    }
}
