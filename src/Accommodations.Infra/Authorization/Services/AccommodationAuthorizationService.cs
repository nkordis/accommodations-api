using Accommodations.App.User;
using Accommodations.Domain.Constants;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Accommodations.Infra.Authorization.Services
{
    public class AccommodationAuthorizationService(ILogger<AccommodationAuthorizationService> logger,
        IUserContext userContext) : IAccommodationAuthorizationService
    {
        public bool Authorize(Accommodation accommodation, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser()!;

            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for accommodation {AccommodationName}",
                user.Email, resourceOperation, accommodation.Name);

            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }

            if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
                && user.Id == accommodation.OwnerId)
            {
                logger.LogInformation("Accommodation owner - successful authorization");
                return true;
            }

            return false;
        }
    }
}
