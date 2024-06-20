using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using Accommodations.Infra.Persistence;

namespace Accommodations.Infra.Repositories
{
    internal class UnitsRepository(AccommodationsDbContext _dbContext) : IUnitsRepository
    {
        public async Task<Guid> Create(Unit entity)
        {
            _dbContext.Units.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
