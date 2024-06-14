using Accommodations.App.Units.Dtos;
using Accommodations.Domain.Entities;

namespace Accommodations.App.Accommodations.Dtos
{
    public class AccommodationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Type { get; set; } = default!;
        public bool HasInstantBooking { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public List<UnitDto> Units { get; set; } = new();

        // <summary>
        /// Maps an Accommodation entity to an AccommodationDto.
        /// </summary>
        /// <param name="accommodation">The Accommodation entity.</param>
        /// <returns>An AccommodationDto.</returns>
        public static AccommodationDto? FromEntity(Accommodation? accommodation)
        {
            if (accommodation == null)
                return null;

            return new AccommodationDto()
            {
                Id = accommodation.Id,
                Name = accommodation.Name,
                Description = accommodation.Description,
                Type = accommodation.Type.ToString(),
                HasInstantBooking = accommodation.HasInstantBooking,
                City = accommodation.Address.City,
                Street = accommodation.Address.Street,
                PostalCode = accommodation.Address.PostalCode,
            };
        }
    }
}
