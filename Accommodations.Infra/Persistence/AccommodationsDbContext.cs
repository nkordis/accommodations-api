using Microsoft.EntityFrameworkCore;
using Accommodations.Domain.Entities;

namespace Accommodations.Infra.Persistence
{
    internal class AccommodationsDbContext(DbContextOptions<AccommodationsDbContext> options) : DbContext(options)
    {
        internal DbSet<Accommodation> Accommodations { get; set; }
        internal DbSet<Unit> Units { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AccommodationsDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Accommodation>()
                .OwnsOne(r => r.Address);

            modelBuilder.Entity<Unit>()
                .OwnsOne(r => r.Address);

            modelBuilder.Entity<Accommodation>()
                .HasMany(r => r.Units)
                .WithOne()
                .HasForeignKey(d => d.AccommodationId);
        }
    }
}
