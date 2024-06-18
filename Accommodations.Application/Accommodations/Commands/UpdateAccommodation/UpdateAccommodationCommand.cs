using MediatR;

namespace Accommodations.App.Accommodations.Commands.UpdateAccommodation
{
    public class UpdateAccommodationCommand : IRequest<bool>
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public string? Type { get; set; } = default!;
        public bool? HasInstantBooking { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
    }
}
