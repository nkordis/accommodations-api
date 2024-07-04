using Accommodations.Domain.Constants;
using Accommodations.Domain.Entities;

namespace Accommodations.Domain.Interfaces
{
    public interface IAccommodationAuthorizationService
    {
        bool Authorize(Accommodation accommodation, ResourceOperation resourceOperation);
    }
}