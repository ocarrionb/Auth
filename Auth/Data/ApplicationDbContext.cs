using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; } = default!;
    }
}
