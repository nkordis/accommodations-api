using Accommodations.Domain.Entities;
using Accommodations.Domain.Exceptions;
using Accommodations.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations.Commands.DeleteAccommodation
{
    public class DeleteAccommodationCommandHandler(ILogger<DeleteAccommodationCommandHandler> logger, 
        IAccommodationsRepository accommodationsRepository) : IRequestHandler<DeleteAccommodationCommand>
    {
        public async Task Handle(DeleteAccommodationCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting accommodation with id : {AccommodationId}", request.Guid);
            var accommodation = await accommodationsRepository.GetAsync(request.Guid);

            if (accommodation is null)
                throw new NotFoundException(nameof(Accommodation), request.Guid.ToString());

            await accommodationsRepository.Delete(accommodation);
        }
    }
}
