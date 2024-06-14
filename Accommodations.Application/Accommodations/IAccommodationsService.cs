using Accommodations.App.Accommodations.Dtos;

namespace Accommodations.App.Accommodations
{
    public interface IAccommodationsService
    {
        Task<AccommodationDto?> GetAccommodation(Guid guid);
        Task<IEnumerable<AccommodationDto>> GetAllAccommodations();
    }
}