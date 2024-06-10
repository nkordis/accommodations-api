using Microsoft.EntityFrameworkCore;
using Accommodations.Domain.Entities;

namespace Accommodations.Infra.Persistence
{
    internal class AccommodationsDbContext : DbContext
    {
        internal DbSet<Accommodation> Accommodations { get; set; }
        internal DbSet<Unit> Units { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AccommodationsDb;Trusted_Connection=True;");
        }
    }
}
