using Microsoft.EntityFrameworkCore;
using SegurosInfinite.Models.User;

namespace SegurosInfinite.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        // Dbsets for database
        public DbSet<User> Users { get; set; }
    }
}
