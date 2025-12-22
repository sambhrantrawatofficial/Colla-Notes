using Microsoft.EntityFrameworkCore;

namespace Colla_Notes.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }

        public DbSet<RegisterClass> RegisterClasss { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
