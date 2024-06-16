using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using Accommodations.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Accommodations.Infra.Repositories
{
    internal class AccommodationsRepository(AccommodationsDbContext _dbContext) : IAccommodationsRepository
    {
        public async Task<Guid> Create(Accommodation entity)
        {
            _dbContext.Accommodations.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<Accommodation>> GetAllAsync()
        {
            var accommodations = await _dbContext.Accommodations.ToListAsync();
            return accommodations;
        }

        public async Task<Accommodation?> GetAsync(Guid guid)
        {
            var accommodation = await _dbContext.Accommodations
                .Include(a => a.Units)
                .FirstOrDefaultAsync(a => a.Id == guid);
            return accommodation;
        }
    }
}
