using Accommodations.App.User;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommodations.App.Accommodations.Commands.CreateAccommodation
{
    public class CreateAccommodationCommandHandler(ILogger<CreateAccommodationCommandHandler> logger,
        IMapper mapper, IAccommodationsRepository accommodationsRepository, IUserContext userContext) 
            : IRequestHandler<CreateAccommodationCommand, Guid>
    {
        public async Task<Guid> Handle(CreateAccommodationCommand request, CancellationToken cancellationToken)
        {
            var currectUser = userContext.GetCurrentUser()!;

            logger.LogInformation("{UserEmail} [{UserId}] is creating a new accommodation {@Accommodation}", 
                currectUser.Email, currectUser.Id, request);

            var accommodation = mapper.Map<Accommodation>(request);
            accommodation.OwnerId = currectUser.Id;

            Guid guid = await accommodationsRepository.Create(accommodation);

            return guid;
        }
    }
}
