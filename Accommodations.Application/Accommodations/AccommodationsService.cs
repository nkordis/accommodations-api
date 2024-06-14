using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations
{
    internal class AccommodationsService(IAccommodationsRepository accommodationsRepository,
        ILogger<AccommodationsService> logger) : IAccommodationsService
    {
        public async Task<IEnumerable<Accommodation>> GetAllAccommodations()
        {
            logger.LogInformation("Getting all accommodations");
            var accommodations = await accommodationsRepository.GetAllAsync();
            return accommodations;
        }

        public async Task<Accommodation?> GetAccommodation(Guid guid)
        {
            logger.LogInformation($"Getting accommodation with guid: {guid}");
            var accommodation = await accommodationsRepository.GetAsync(guid);
            return accommodation;
        }
    }
}
