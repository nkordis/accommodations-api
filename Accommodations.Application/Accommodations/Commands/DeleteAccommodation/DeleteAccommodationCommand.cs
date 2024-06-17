using MediatR;

namespace Accommodations.App.Accommodations.Commands.DeleteAccommodation
{
    public class DeleteAccommodationCommand(Guid guid) : IRequest<bool>
    {
        public Guid Guid { get; } = guid;
    }
}
