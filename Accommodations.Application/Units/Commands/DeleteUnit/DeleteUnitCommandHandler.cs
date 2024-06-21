using Accommodations.Domain.Exceptions;
using Accommodations.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Units.Commands.DeleteUnit
{
    public class DeleteUnitCommandHandler(ILogger<DeleteUnitCommandHandler> logger,
        IAccommodationsRepository accommodationsRepository, IUnitsRepository unitsRepository) : IRequestHandler<DeleteUnitCommand>
    {
        public async Task Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting Unit with id {UnitId}, " +
                "from accommodation with id : {AccommodationId}", request.Guid, request.AccommodationId);

            var accommodation = await accommodationsRepository.GetAsync(request.AccommodationId);
            if (accommodation == null) 
                throw new NotFoundException(nameof(accommodation), request.AccommodationId.ToString());
            
            var unit = accommodation.Units.FirstOrDefault();

            if (unit == null)
                throw new NotFoundException(nameof(unit), request.Guid.ToString());

            await unitsRepository.Delete(unit);
        }
    }
}
