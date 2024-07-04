using Microsoft.AspNetCore.Authorization;

namespace Accommodations.Infra.Authorization.Requirements
{
    public class CreateMultipleAccommodationRequirement(int minimumAccommodationsCreated) 
        : IAuthorizationRequirement
    {
        public int MinimumAccommodationsCreated { get; } = minimumAccommodationsCreated;
    }
}
