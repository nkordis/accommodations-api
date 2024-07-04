using Microsoft.EntityFrameworkCore;
using Accommodations.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Accommodations.Infra.Persistence
{
    internal class AccommodationsDbContext(DbContextOptions<AccommodationsDbContext> options) 
        : IdentityDbContext<User>(options)
    {
        internal DbSet<Accommodation> Accommodations { get; set; }
        internal DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Accommodation>()
                .OwnsOne(a => a.Address);

            modelBuilder.Entity<Unit>()
                .OwnsOne(a => a.Address);

            modelBuilder.Entity<Accommodation>()
                .HasMany(a => a.Units)
                .WithOne()
                .HasForeignKey(un => un.AccommodationId);

            modelBuilder.Entity<User>()
                .HasMany(usr => usr.OwnedAccommodations)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId);
        }
    }
}
