using Accommodations.App.Accommodations.Dtos;
using Accommodations.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations.Queries.GetAccommodationById
{
    public class GetAccommodationByIdQueryHandler(ILogger<GetAccommodationByIdQueryHandler> logger,
        IMapper mapper, IAccommodationsRepository accommodationsRepository) : IRequestHandler<GetAccommodationByIdQuery, AccommodationDto?>
    {
        public async Task<AccommodationDto?> Handle(GetAccommodationByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting accommodation with {AccommodationId}", request.Guid);
            var accommodation = await accommodationsRepository.GetAsync(request.Guid);
            var accommodationdto = mapper.Map<AccommodationDto?>(accommodation);

            return accommodationdto;
        }
    }
}
