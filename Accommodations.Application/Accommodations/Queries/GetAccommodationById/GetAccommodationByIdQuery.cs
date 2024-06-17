using Accommodations.App.Accommodations.Dtos;
using MediatR;

namespace Accommodations.App.Accommodations.Queries.GetAccommodationById
{
    public class GetAccommodationByIdQuery(Guid guid) : IRequest<AccommodationDto?>
    {
        public Guid Guid { get; } = guid;
    }
}
