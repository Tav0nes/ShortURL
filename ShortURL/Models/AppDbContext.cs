using Microsoft.EntityFrameworkCore;

namespace ShortURL.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<URL> URLs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<URL>()
                .HasKey(u => u.OriginalURL); // Specify OriginalURL as the primary key
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=testdb;User=testuser;Password=testpassword;");
        }
    }
}
