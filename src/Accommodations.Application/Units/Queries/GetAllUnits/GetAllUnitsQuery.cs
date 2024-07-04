using Accommodations.App.Units.Dtos;
using MediatR;

namespace Accommodations.App.Units.Queries.GetAllUnits
{
    public class GetAllUnitsQuery(Guid guid) : IRequest<IEnumerable<UnitDto>>
    {
        public Guid AccommodationId { get; } = guid;
    }
}
