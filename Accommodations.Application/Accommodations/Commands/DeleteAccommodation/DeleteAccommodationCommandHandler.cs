using Accommodations.App.Accommodations.Commands.CreateAccommodation;
using Accommodations.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations.Commands.DeleteAccommodation
{
    public class DeleteAccommodationCommandHandler(ILogger<CreateAccommodationCommandHandler> logger, 
        IAccommodationsRepository accommodationsRepository) : IRequestHandler<DeleteAccommodationCommand, bool>
    {
        public async Task<bool> Handle(DeleteAccommodationCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Deleting accommodation with id {request.Guid}");
            var accommodation = await accommodationsRepository.GetAsync(request.Guid);

            if (accommodation is null) 
                return false;

            await accommodationsRepository.Delete(accommodation);

            return true;
        }
    }
}
