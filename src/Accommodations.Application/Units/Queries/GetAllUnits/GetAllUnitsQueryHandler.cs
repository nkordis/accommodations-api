using Accommodations.App.Units.Dtos;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Exceptions;
using Accommodations.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Units.Queries.GetAllUnits
{
    public class GetAllUnitsQueryHandler(ILogger<GetAllUnitsQueryHandler> logger, IMapper mapper, 
        IAccommodationsRepository accommodationsRepository, IUnitsRepository unitsRepository) 
        : IRequestHandler<GetAllUnitsQuery, IEnumerable<UnitDto>>
    {
        public async Task<IEnumerable<UnitDto>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all units for accommodation with id: {AccommodationId}", request.AccommodationId);
            var accommodation = await accommodationsRepository.GetAsync(request.AccommodationId);
            if (accommodation == null) 
                throw new NotFoundException(nameof(accommodation), request.AccommodationId.ToString());

            var unitsDto = mapper.Map<IEnumerable<UnitDto>>(accommodation.Units);

            return unitsDto;
        }
    }
}
