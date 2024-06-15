using Accommodations.Domain.Entities;

namespace Accommodations.App.Units.Dtos
{
    public class UnitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public BillingPeriod BillingPeriod { get; set; } = default!;
        public int Capacity { get; set; }
        public UnitType Type { get; set; } = default!;
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
