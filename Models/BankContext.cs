using Microsoft.EntityFrameworkCore;
 
namespace connectingToDBTESTING.Models
{
    public class BankContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}