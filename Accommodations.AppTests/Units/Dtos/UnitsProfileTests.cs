using AutoMapper;
using Accommodations.App.Units.Commands.CreateUnit;
using Accommodations.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Accommodations.App.Units.Dtos.Tests
{
    public class UnitsProfileTests
    {
        private readonly IMapper _mapper;

        public UnitsProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UnitsProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void CreateMap_ForCreateUnitCommandToUnit_MapsCorrectly()
        {
            // Arrange
            var command = new CreateUnitCommand
            {
                AccommodationId = Guid.NewGuid(),
                Name = "Luxury Suite",
                Description = "A spacious and luxurious suite.",
                Price = 500.50m,
                BillingPeriod = "PerDay",
                Capacity = 4,
                Type = "Suite",
                City = "New York",
                Street = "5th Avenue",
                PostalCode = "10001",
                IsAvailable = true
            };

            // Act
            var unit = _mapper.Map<Unit>(command);

            // Assert
            unit.Should().NotBeNull();
            unit.AccommodationId.Should().Be(command.AccommodationId);
            unit.Name.Should().Be(command.Name);
            unit.Description.Should().Be(command.Description);
            unit.Price.Should().Be(command.Price);
            unit.BillingPeriod.Should().Be(BillingPeriod.PerDay);
            unit.Capacity.Should().Be(command.Capacity);
            unit.Type.Should().Be(UnitType.Suite);
            unit.IsAvailable.Should().Be(command.IsAvailable);
            unit.Address.Should().NotBeNull();
            unit.Address.City.Should().Be(command.City);
            unit.Address.Street.Should().Be(command.Street);
            unit.Address.PostalCode.Should().Be(command.PostalCode);
        }

        [Fact]
        public void CreateMap_ForUnitToUnitDto_MapsCorrectly()
        {
            // Arrange
            var unit = new Unit
            {
                Id = Guid.NewGuid(),
                Name = "Luxury Suite",
                Description = "A spacious and luxurious suite.",
                Price = 500.50m,
                BillingPeriod = BillingPeriod.PerDay,
                Capacity = 4,
                Type = UnitType.Suite,
                Address = new Address
                {
                    City = "New York",
                    Street = "5th Avenue",
                    PostalCode = "10001"
                },
                IsAvailable = true,
                AccommodationId = Guid.NewGuid()
            };

            // Act
            var unitDto = _mapper.Map<UnitDto>(unit);

            // Assert
            unitDto.Should().NotBeNull();
            unitDto.Id.Should().Be(unit.Id);
            unitDto.Name.Should().Be(unit.Name);
            unitDto.Description.Should().Be(unit.Description);
            unitDto.Price.Should().Be(unit.Price);
            unitDto.BillingPeriod.Should().Be(unit.BillingPeriod);
            unitDto.Capacity.Should().Be(unit.Capacity);
            unitDto.Type.Should().Be(unit.Type);
            unitDto.IsAvailable.Should().Be(unit.IsAvailable);
            unitDto.City.Should().Be(unit.Address.City);
            unitDto.Street.Should().Be(unit.Address.Street);
            unitDto.PostalCode.Should().Be(unit.Address.PostalCode);
        }

        [Fact]
        public void CreateMap_ForUnitToUnitDto_WhenAddressIsNull_ShouldMapCorrectly()
        {
            // Arrange
            var unit = new Unit
            {
                Id = Guid.NewGuid(),
                Name = "Luxury Suite",
                Description = "A spacious and luxurious suite.",
                Price = 500.50m,
                BillingPeriod = BillingPeriod.PerDay,
                Capacity = 4,
                Type = UnitType.Suite,
                Address = null,
                IsAvailable = true,
                AccommodationId = Guid.NewGuid()
            };

            // Act
            var unitDto = _mapper.Map<UnitDto>(unit);

            // Assert
            unitDto.Should().NotBeNull();
            unitDto.Id.Should().Be(unit.Id);
            unitDto.Name.Should().Be(unit.Name);
            unitDto.Description.Should().Be(unit.Description);
            unitDto.Price.Should().Be(unit.Price);
            unitDto.BillingPeriod.Should().Be(unit.BillingPeriod);
            unitDto.Capacity.Should().Be(unit.Capacity);
            unitDto.Type.Should().Be(unit.Type);
            unitDto.IsAvailable.Should().Be(unit.IsAvailable);
            unitDto.City.Should().BeNull();
            unitDto.Street.Should().BeNull();
            unitDto.PostalCode.Should().BeNull();
        }
    }
}
