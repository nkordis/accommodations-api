using Accommodations.Domain.Entities;
using Accommodations.Domain.Exceptions;
using Accommodations.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations.Commands.UpdateAccommodation
{
    public class UpdateAccommodationCommandHandler(ILogger<UpdateAccommodationCommandHandler> logger, 
        IMapper mapper, IAccommodationsRepository accommodationsRepository) : IRequestHandler<UpdateAccommodationCommand>
    {
        public async Task Handle(UpdateAccommodationCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating accommodation with id: {AccommodationId} with {@UpdateAccommodation}", request.Guid, request);
            var accommodation = await accommodationsRepository.GetAsync(request.Guid);

            if (accommodation is null)
                throw new NotFoundException(nameof(Accommodation), request.Guid.ToString());

            mapper.Map(request, accommodation);

            await accommodationsRepository.SaveChanges();
        }
    }
}
