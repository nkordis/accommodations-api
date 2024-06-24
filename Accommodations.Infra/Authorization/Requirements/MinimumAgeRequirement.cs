using Microsoft.AspNetCore.Authorization;

namespace Accommodations.Infra.Authorization.Requirements
{
    public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
    {
        public int MinimumAge { get; } = minimumAge;
    }
}
