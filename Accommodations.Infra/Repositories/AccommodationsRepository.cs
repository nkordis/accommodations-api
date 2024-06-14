using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using Accommodations.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Accommodations.Infra.Repositories
{
    internal class AccommodationsRepository(AccommodationsDbContext _dbContext) : IAccommodationsRepository
    {
        public async Task<IEnumerable<Accommodation>> GetAllAsync()
        {
            var accommodations = await _dbContext.Accommodations.ToListAsync();
            return accommodations;
        }

        public async Task<Accommodation?> GetAsync(Guid guid)
        {
            var accommodation = await _dbContext.Accommodations.FirstOrDefaultAsync(a => a.Id == guid);
            return accommodation;
        }
    }
}
