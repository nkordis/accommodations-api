using Accommodations.App.Accommodations.Commands.CreateAccommodation;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Exceptions;
using Accommodations.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Units.Commands.CreateUnit
{
    public class CreateUnitCommandHandler(ILogger<CreateUnitCommandHandler> logger,
        IMapper mapper, IAccommodationsRepository accommodationsRepository, IUnitsRepository unitsRepository) 
        : IRequestHandler<CreateUnitCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new unit: {@Unit}", request);
            var accommodation = await accommodationsRepository.GetAsync(request.AccommodationId);
            if (accommodation == null) 
                throw new NotFoundException(nameof(Accommodation), request.AccommodationId.ToString());

            var unit = mapper.Map<Domain.Entities.Unit>(request);
            Guid guid = await unitsRepository.Create(unit);

            return guid;
        }
    }
}
