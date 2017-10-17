using Microsoft.EntityFrameworkCore;
 
namespace connectingToDBTESTING.Models
{
    public class TransactionContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }
        public DbSet<Transaction> Transactions { get; set; }
    }
}