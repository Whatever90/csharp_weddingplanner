using Microsoft.EntityFrameworkCore;
 
namespace connectingToDBTESTING.Models
{
    public class TestContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public TestContext(DbContextOptions<TestContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}