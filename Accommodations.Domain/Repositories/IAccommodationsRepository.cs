using Accommodations.Domain.Entities;

namespace Accommodations.Domain.Repositories
{
    public interface IAccommodationsRepository
    {
        Task<IEnumerable<Accommodation>> GetAllAsync();
        Task<Accommodation?> GetAsync(Guid guid);
        Task<Guid> Create(Accommodation entity);
        Task Delete(Accommodation entity);
        Task SaveChanges();
        Task<IEnumerable<Accommodation>> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber);
    }
}
