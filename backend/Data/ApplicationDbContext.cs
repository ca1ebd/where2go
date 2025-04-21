using Microsoft.EntityFrameworkCore;
using Where2Go.API.Models;

namespace Where2Go.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Place>()
                .HasIndex(p => p.ShareableUrl)
                .IsUnique();
        }
    }
} 