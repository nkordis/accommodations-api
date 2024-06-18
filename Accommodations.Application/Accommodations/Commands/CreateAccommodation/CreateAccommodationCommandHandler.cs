using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations.Commands.CreateAccommodation
{
    public class CreateAccommodationCommandHandler(ILogger<CreateAccommodationCommandHandler> logger,
        IMapper mapper, IAccommodationsRepository accommodationsRepository) : IRequestHandler<CreateAccommodationCommand, Guid>
    {
        public async Task<Guid> Handle(CreateAccommodationCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new accommodation {@Accommodation}", request);
            var accommodation = mapper.Map<Accommodation>(request);
            Guid guid = await accommodationsRepository.Create(accommodation);

            return guid;
        }
    }
}
