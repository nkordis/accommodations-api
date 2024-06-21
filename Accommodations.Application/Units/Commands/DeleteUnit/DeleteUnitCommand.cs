using MediatR;

namespace Accommodations.App.Units.Commands.DeleteUnit
{
    public class DeleteUnitCommand(Guid accommodationId, Guid guid) : IRequest
    {
        public Guid AccommodationId { get; } = accommodationId;
        public Guid Guid { get; } = guid;
    }
}
