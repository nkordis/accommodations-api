using Accommodations.App.User;
using Accommodations.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Accommodations.Infra.Authorization.Requirements
{
    public class CreateMultipleAccommodationRequirementHandler(IAccommodationsRepository accommodationsRepository,
        IUserContext userContext)
            : AuthorizationHandler<CreateMultipleAccommodationRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleAccommodationRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser()!;
            var accommodations = await accommodationsRepository.GetAllAsync();

            var userAccommodationsCreated = accommodations.Count(a => a.OwnerId == currentUser.Id);

            if (userAccommodationsCreated >= requirement.MinimumAccommodationsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
