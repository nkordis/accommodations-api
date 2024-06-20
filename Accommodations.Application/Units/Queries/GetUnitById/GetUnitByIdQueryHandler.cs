using Accommodations.App.Units.Dtos;
using Accommodations.Domain.Exceptions;
using Accommodations.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Units.Queries.GetUnitById
{
    public class GetUnitByIdQueryHandler(ILogger<GetUnitByIdQueryHandler> logger, IMapper mapper, 
        IAccommodationsRepository accommodationsRepository) : IRequestHandler<GetUnitByIdQuery, UnitDto>
    {
        public async Task<UnitDto> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {

            logger.LogInformation("Getting unit with id: {UnitId} for accommodation with id: {AccommodationId}", request.Guid, request.AccommodationId);
            var accommodation = await accommodationsRepository.GetAsync(request.AccommodationId);
            if (accommodation == null)
                throw new NotFoundException(nameof(accommodation), request.AccommodationId.ToString());

            var unit = accommodation.Units.FirstOrDefault(u => u.Id == request.Guid);
            if (unit == null)
                throw new NotFoundException(nameof(unit), request.Guid.ToString());



            var unitDto = mapper.Map<UnitDto>(unit);

            return unitDto;
        }
    }
}
