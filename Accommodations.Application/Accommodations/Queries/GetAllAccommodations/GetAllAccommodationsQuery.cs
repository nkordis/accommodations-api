using Accommodations.App.Accommodations.Dtos;
using MediatR;

namespace Accommodations.App.Accommodations.Queries.GetAllAccommodations
{
    public class GetAllAccommodationsQuery : IRequest<IEnumerable<AccommodationDto>>
    {
    }
}
