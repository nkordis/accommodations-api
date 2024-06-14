using Accommodations.Domain.Entities;

namespace Accommodations.App.Accommodations
{
    public interface IAccommodationsService
    {
        Task<Accommodation?> GetAccommodation(Guid guid);
        Task<IEnumerable<Accommodation>> GetAllAccommodations();
    }
}