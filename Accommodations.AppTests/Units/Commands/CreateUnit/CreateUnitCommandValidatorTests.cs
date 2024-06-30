using FluentValidation.TestHelper;
using Xunit;

namespace Accommodations.App.Units.Commands.CreateUnit.Tests
{
    public class CreateUnitCommandValidatorTests
    {
        [Fact]
        public void ValidatorForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new CreateUnitCommand
            {
                AccommodationId = Guid.NewGuid(),
                Name = "Luxury Suite",
                Description = "A luxurious suite with a beautiful ocean view.",
                Price = 999.99m,
                BillingPeriod = "PerDay",
                Capacity = 2,
                Type = "Suite",
                City = "Miami",
                Street = "123 Ocean Drive",
                PostalCode = "33139",
                IsAvailable = true
            };

            var validator = new CreateUnitCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ValidatorForInvalidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var command = new CreateUnitCommand
            {
                AccommodationId = Guid.NewGuid(),
                Name = "",
                Description = "",
                Price = -1,
                BillingPeriod = "InvalidPeriod",
                Capacity = 0,
                Type = "InvalidType",
                City = new string('a', 101),
                Street = new string('a', 101),
                PostalCode = new string('a', 51),
                IsAvailable = true
            };

            var validator = new CreateUnitCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.Price);
            result.ShouldHaveValidationErrorFor(x => x.BillingPeriod);
            result.ShouldHaveValidationErrorFor(x => x.Capacity);
            result.ShouldHaveValidationErrorFor(x => x.Type);
            result.ShouldHaveValidationErrorFor(x => x.City);
            result.ShouldHaveValidationErrorFor(x => x.Street);
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }

        [Theory]
        [InlineData("PerDay")]
        [InlineData("PerWeek")]
        [InlineData("PerMonth")]
        [InlineData("PerYear")]
        public void ValidatorForValidBillingPeriod_ShouldNotHaveValidationErrors(string validBillingPeriod)
        {
            // Arrange
            var command = new CreateUnitCommand
            {
                AccommodationId = Guid.NewGuid(),
                Name = "Luxury Suite",
                Description = "A luxurious suite with a beautiful ocean view.",
                Price = 999.99m,
                BillingPeriod = validBillingPeriod,
                Capacity = 2,
                Type = "Suite",
                City = "Miami",
                Street = "123 Ocean Drive",
                PostalCode = "33139",
                IsAvailable = true
            };

            var validator = new CreateUnitCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.BillingPeriod);
        }

        [Theory]
        [InlineData("InvalidPeriod")]
        [InlineData("")]
        public void ValidatorForInvalidBillingPeriod_ShouldHaveValidationErrors(string invalidBillingPeriod)
        {
            // Arrange
            var command = new CreateUnitCommand
            {
                AccommodationId = Guid.NewGuid(),
                Name = "Luxury Suite",
                Description = "A luxurious suite with a beautiful ocean view.",
                Price = 999.99m,
                BillingPeriod = invalidBillingPeriod,
                Capacity = 2,
                Type = "Suite",
                City = "Miami",
                Street = "123 Ocean Drive",
                PostalCode = "33139",
                IsAvailable = true
            };

            var validator = new CreateUnitCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BillingPeriod);
        }

        [Theory]
        [InlineData("Room")]
        [InlineData("Suite")]
        [InlineData("Apartment")]
        [InlineData("Villa")]
        [InlineData("Cottage")]
        [InlineData("Dormitory")]
        [InlineData("Other")]
        public void ValidatorForValidUnitType_ShouldNotHaveValidationErrors(string validUnitType)
        {
            // Arrange
            var command = new CreateUnitCommand
            {
                AccommodationId = Guid.NewGuid(),
                Name = "Luxury Suite",
                Description = "A luxurious suite with a beautiful ocean view.",
                Price = 999.99m,
                BillingPeriod = "PerDay",
                Capacity = 2,
                Type = validUnitType,
                City = "Miami",
                Street = "123 Ocean Drive",
                PostalCode = "33139",
                IsAvailable = true
            };

            var validator = new CreateUnitCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Type);
        }

        [Theory]
        [InlineData("InvalidType")]
        [InlineData("")]
        public void ValidatorForInvalidUnitType_ShouldHaveValidationErrors(string invalidUnitType)
        {
            // Arrange
            var command = new CreateUnitCommand
            {
                AccommodationId = Guid.NewGuid(),
                Name = "Luxury Suite",
                Description = "A luxurious suite with a beautiful ocean view.",
                Price = 999.99m,
                BillingPeriod = "PerDay",
                Capacity = 2,
                Type = invalidUnitType,
                City = "Miami",
                Street = "123 Ocean Drive",
                PostalCode = "33139",
                IsAvailable = true
            };

            var validator = new CreateUnitCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Type);
        }
    }
}
