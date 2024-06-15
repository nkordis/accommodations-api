using Accommodations.Domain.Entities;

namespace Accommodations.App.Units.Dtos
{
    public class UnitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string BillingPeriod { get; set; } = default!;
        public int Capacity { get; set; }
        public string Type { get; set; } = default!;
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public bool IsAvailable { get; set; } = true;

        /// <summary>
        /// Converts a Unit entity to a UnitDto.
        /// </summary>
        /// <param name="unit">The unit entity to convert.</param>
        /// <returns>A UnitDto representing the unit entity.</returns>
        internal static UnitDto FromEntity(Unit unit)
        {
            return new UnitDto()
            {
                Id = unit.Id,
                Name = unit.Name,
                Description = unit.Description,
                Price = unit.Price,
                BillingPeriod = unit.BillingPeriod.ToString(),
                Capacity = unit.Capacity,
                Type = unit.Type.ToString(),
                City = unit.Address?.City,
                Street = unit.Address?.Street,
                PostalCode = unit.Address?.PostalCode,
                IsAvailable = unit.IsAvailable,
            };
        }
    }
}
