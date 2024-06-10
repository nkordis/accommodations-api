
namespace Accommodations.Domain.Entities
{
    /// <summary>
    /// Represents an address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        public string? Street { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        public string? PostalCode { get; set; }
    }
}
