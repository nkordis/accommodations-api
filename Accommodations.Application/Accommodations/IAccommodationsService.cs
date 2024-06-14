using Accommodations.Domain.Entities;

namespace Accommodations.App.Accommodations
{
    public interface IAccommodationsService
    {
        Task<IEnumerable<Accommodation>> GetAllAccommodations();
    }
}