using Microsoft.EntityFrameworkCore;
using SegurosInfinite.Models;
using SegurosInfinite.Models.User;

namespace SegurosInfinite.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        // Dbsets for database
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coverage> Coverages { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
    }
}
