using Microsoft.EntityFrameworkCore;
using SharedModels.Models;

namespace ShortUrlWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<URL> Urls { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<URL>()
                .HasKey(u => u.OriginalURL);
        }
    }
}
