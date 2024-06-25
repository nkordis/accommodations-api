
namespace Accommodations.Domain.Entities
{
    /// <summary>
    /// Represents an accommodation entity.
    /// </summary>
    public class Accommodation
    {
        /// <summary>
        /// Gets or sets the unique identifier for the accommodation.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the accommodation.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the description of the accommodation.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Gets or sets the type of accommodation.
        /// </summary>
        public AccommodationType Type { get; set; } // Indicates the type of rental business

        /// <summary>
        /// Gets or sets a value indicating whether the accommodation supports instant booking.
        /// </summary>
        public bool HasInstantBooking { get; set; }

        /// <summary>
        /// Gets or sets the contact email for the accommodation.
        /// </summary>
        public string? ContactEmail { get; set; }

        /// <summary>
        /// Gets or sets the contact number for the accommodation.
        /// </summary>
        public string? ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the address of the accommodation.
        /// </summary>
        public Address Address { get; set; } = default!;

        /// <summary>
        /// Gets or sets the list of units within the accommodation.
        /// </summary>
        public List<Unit> Units { get; set; } = new();

        /// <summary>
        /// Gets or sets the Owner of the accommodation.
        /// </summary>
        public User Owner { get; set; } = default!;

        /// <summary>
        /// Gets or sets the Owner's Id of the accommodation
        /// </summary>
        public string OwnerId { get; set; } = default!;
    }

    /// <summary>
    /// Enumerates the types of accommodations.
    /// </summary>
    public enum AccommodationType
    {
        Hotel,
        Resort,
        BedAndBreakfast,
        Hostel,
        IndividualOwner,
        GuestHouse,
        ApartmentComplex,
        Other
    }
}





