using Accommodations.Domain.Constants;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Exceptions;
using Accommodations.Domain.Interfaces;
using Accommodations.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations.Commands.UploadAccommodationImage
{
    public class UploadAccommodationImageCommandHandler(ILogger<UploadAccommodationImageCommandHandler> logger, 
        IAccommodationsRepository accommodationsRepository, 
        IAccommodationAuthorizationService accommodationAuthorizationService,
        IBlobStorageService blobStorageService) 
            : IRequestHandler<UploadAccommodationImageCommand>
    {
        public async Task Handle(UploadAccommodationImageCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Uploading accommodation image for id: {AccommodationId}", request.Guid);
            var accommodation = await accommodationsRepository.GetAsync(request.Guid);

            if (accommodation is null)
                throw new NotFoundException(nameof(Accommodation), request.Guid.ToString());

            if (!accommodationAuthorizationService.Authorize(accommodation, ResourceOperation.Update))
                throw new ForbidException();

            var imageUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);
            accommodation.ImageUrl = imageUrl;

            await accommodationsRepository.SaveChanges();
        }
    }
}
