using Microsoft.EntityFrameworkCore;
 
namespace connectingToDBTESTING.Models
{
    public class RevContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public RevContext(DbContextOptions<RevContext> options) : base(options) { }
        public DbSet<Review> Reviews { get; set; }
    }
}