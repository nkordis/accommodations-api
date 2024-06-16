using Accommodations.App.Accommodations.Dtos;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations
{
    internal class AccommodationsService(IAccommodationsRepository accommodationsRepository,
        ILogger<AccommodationsService> logger, IMapper mapper) : IAccommodationsService
    {
        public async Task<IEnumerable<AccommodationDto>> GetAllAccommodations()
        {
            logger.LogInformation("Getting all accommodations");
            var accommodations = await accommodationsRepository.GetAllAsync();
            var accommodationsdto = mapper.Map<IEnumerable<AccommodationDto>>(accommodations);

            return accommodationsdto!;
        }

        public async Task<AccommodationDto?> GetAccommodation(Guid guid)
        {
            logger.LogInformation($"Getting accommodation with guid: {guid}");
            var accommodation = await accommodationsRepository.GetAsync(guid);
            var accommodationdto = mapper.Map<AccommodationDto?>(accommodation);

            return accommodationdto;
        }

        public async Task<Guid> Create(CreateAccommodationDto dto)
        {
            logger.LogInformation("Creating a new accommodation");
            var accommodation = mapper.Map<Accommodation>(dto);
            Guid guid = await accommodationsRepository.Create(accommodation);

            return guid;
        }
    }
}
