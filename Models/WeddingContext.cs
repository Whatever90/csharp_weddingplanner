using Microsoft.EntityFrameworkCore;
 
namespace connectingToDBTESTING.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }
        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Guest> Guests { get; set; }
    }
}