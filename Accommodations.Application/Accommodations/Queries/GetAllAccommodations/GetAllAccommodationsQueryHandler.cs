using Accommodations.App.Accommodations.Dtos;
using Accommodations.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations.Queries.GetAllAccommodations
{
    public class GetAllAccommodationsQueryHandler(ILogger<GetAllAccommodationsQueryHandler> logger,
        IMapper mapper, IAccommodationsRepository accommodationsRepository) : IRequestHandler<GetAllAccommodationsQuery,
        IEnumerable<AccommodationDto>>
    {
        public async Task<IEnumerable<AccommodationDto>> Handle(GetAllAccommodationsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all accommodations");
            var accommodations = await accommodationsRepository.GetAllMatchingAsync(request.SearchPhrase);
            var accommodationsdto = mapper.Map<IEnumerable<AccommodationDto>>(accommodations);

            return accommodationsdto!;
        }
    }
}
