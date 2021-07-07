using HOSAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HOSAPI.Contexts
{
    public class SocarDbContext : DbContext
    {
        public SocarDbContext(DbContextOptions<SocarDbContext> options) : base(options) { }

        public virtual DbSet<Comment> Comments { get; set; }
        // public virtual DbSet<User> User { get; set; }

    }
}
