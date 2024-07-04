using Accommodations.Domain.Constants;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using Accommodations.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task Delete(Accommodation entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Accommodation>> GetAllAsync()
        {
            var accommodations = await _dbContext.Accommodations.ToListAsync();
            return accommodations;
        }

        public async Task<(IEnumerable<Accommodation>, int)> GetAllMatchingAsync(string? searchPhrase, 
            int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = _dbContext
                .Accommodations
                .Where(a => searchPhraseLower == null || (a.Name.ToLower().Contains(searchPhraseLower)
                                                       || a.Description.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();

            if(sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Accommodation, object>>>
                {
                    {nameof(Accommodation.Name), a => a.Name },
                    {nameof(Accommodation.Description), a => a.Description },
                    {nameof(Accommodation.Type), a => a.Type },
                    {nameof(Accommodation.Address.City), a => a.Address.City ?? "" }
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending 
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var accommodations = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (accommodations, totalCount);
        }

        public async Task<Accommodation?> GetAsync(Guid guid)
        {
            var accommodation = await _dbContext.Accommodations
                .Include(a => a.Units)
                .FirstOrDefaultAsync(a => a.Id == guid);
            return accommodation;
        }
        public Task SaveChanges()
            => _dbContext.SaveChangesAsync();
    }
}
