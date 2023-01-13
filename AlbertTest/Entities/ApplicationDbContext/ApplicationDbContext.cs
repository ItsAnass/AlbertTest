using Microsoft.EntityFrameworkCore;

namespace Albert.BackendChallenge.Entities.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Reservation> Reservation { get; set; }

    }
}
