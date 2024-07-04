

using Accommodations.App.Units.Dtos;
using MediatR;

namespace Accommodations.App.Units.Queries.GetUnitById
{
    public class GetUnitByIdQuery(Guid accommodationId, Guid guid) : IRequest<UnitDto>
    {
        public Guid AccommodationId { get; } = accommodationId;
        public Guid Guid { get; } = guid;
}
}
