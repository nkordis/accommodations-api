using Accommodations.Domain.Entities;

namespace Accommodations.Domain.Repositories
{
    public interface IUnitsRepository
    {
        Task<Guid> Create(Unit entity);
    }
}
