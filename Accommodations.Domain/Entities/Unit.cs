
namespace Accommodations.Domain.Entities
{
    /// <summary>
    /// Represents a unit within an accommodation.
    /// </summary>
    public class Unit
    {
        /// <summary>
        /// Gets or sets the unique identifier for the unit.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the unit.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Gets or sets the description of the unit.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Gets or sets the price of the unit.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the billing period for the unit.
        /// </summary>
        public BillingPeriod BillingPeriod { get; set; }

        /// <summary>
        /// Gets or sets the capacity of the unit.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the type of the unit.
        /// </summary>
        public UnitType Type { get; set; } // Indicates the type of unit

        /// <summary>
        /// Gets or sets the address of the unit.
        /// </summary>
        public Address? Address { get; set; } // Each Unit can have its own Address

        /// <summary>
        /// Gets or sets the unique identifier of the accommodation that the unit belongs to.
        /// </summary>
        public Guid AccommodationId { get; set; }
    }

    /// <summary>
    /// Enumerates the billing periods for units.
    /// </summary>
    public enum BillingPeriod
    {
        PerDay,
        PerWeek,
        PerMonth,
        PerYear
    }

    /// <summary>
    /// Enumerates the types of units.
    /// </summary>
    public enum UnitType
    {
        Room,
        Suite,
        Apartment,
        Villa,
        Cottage,
        Dormitory,
        Other
    }
}

