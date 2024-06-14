using Accommodations.App.Accommodations.Dtos;
using Accommodations.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations
{
    internal class AccommodationsService(IAccommodationsRepository accommodationsRepository,
        ILogger<AccommodationsService> logger) : IAccommodationsService
    {
        public async Task<IEnumerable<AccommodationDto>> GetAllAccommodations()
        {
            logger.LogInformation("Getting all accommodations");
            var accommodations = await accommodationsRepository.GetAllAsync();
            var accommodationsdto = accommodations.Select(AccommodationDto.FromEntity).ToList();
            return accommodationsdto!;
        }

        public async Task<AccommodationDto?> GetAccommodation(Guid guid)
        {
            logger.LogInformation($"Getting accommodation with guid: {guid}");
            var accommodation = await accommodationsRepository.GetAsync(guid);
            var accommodationdto = AccommodationDto.FromEntity(accommodation);
            return accommodationdto;
        }
    }
}
