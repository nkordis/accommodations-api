using Accommodations.App.Units.Dtos;
using Accommodations.Domain.Entities;

namespace Accommodations.App.Accommodations.Dtos
{
    public class AccommodationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public AccommodationType Type { get; set; } = default!;
        public bool HasInstantBooking { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public List<UnitDto> Units { get; set; } = [];
    }
}
