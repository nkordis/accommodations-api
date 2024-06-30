using FluentValidation.TestHelper;
using Xunit;

namespace Accommodations.App.Accommodations.Commands.UpdateAccommodation.Tests
{
    public class UpdateAccommodationCommandValidatorTests
    {
        [Fact]
        public void ValidatorForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new UpdateAccommodationCommand
            {
                Guid = Guid.NewGuid(),
                Name = "Updated Mountain View Cabin",
                Description = "An updated description for the cozy cabin with a breathtaking view of the mountains.",
                Type = "GuestHouse",
                HasInstantBooking = true,
                ContactEmail = "contact@mountainviewcabin.com",
                ContactNumber = "+123456789"
            };

            var validator = new UpdateAccommodationCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ValidatorForCommandWithNoFieldsPopulated_ShouldHaveValidationError()
        {
            // Arrange
            var command = new UpdateAccommodationCommand();

            var validator = new UpdateAccommodationCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x)
                .WithErrorMessage("At least one field must be populated.");
        }

        [Fact]
        public void ValidatorForInvalidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var command = new UpdateAccommodationCommand
            {
                Guid = Guid.NewGuid(),
                Name = "A",
                Description = "B",
                Type = "InvalidType",
                HasInstantBooking = null,
                ContactEmail = "invalid-email",
                ContactNumber = "12345"
            };

            var validator = new UpdateAccommodationCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.Type);
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
            result.ShouldHaveValidationErrorFor(x => x.ContactNumber);
        }

        [Theory]
        [InlineData("Hotel")]
        [InlineData("Resort")]
        [InlineData("BedAndBreakfast")]
        [InlineData("Hostel")]
        [InlineData("IndividualOwner")]
        [InlineData("GuestHouse")]
        [InlineData("ApartmentComplex")]
        [InlineData("Other")]
        public void ValidatorForValidAccommodationType_ShouldNotHaveValidationErrors(string validType)
        {
            // Arrange
            var command = new UpdateAccommodationCommand
            {
                Guid = Guid.NewGuid(),
                Name = "Valid Name",
                Description = "Valid Description",
                Type = validType,
                HasInstantBooking = true,
                ContactEmail = "contact@valid.com",
                ContactNumber = "+123456789"
            };

            var validator = new UpdateAccommodationCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Type);
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("invalid.com")]
        [InlineData("@invalid.com")]
        public void ValidatorForInvalidEmail_ShouldHaveValidationErrors(string invalidEmail)
        {
            // Arrange
            var command = new UpdateAccommodationCommand
            {
                Guid = Guid.NewGuid(),
                Name = "Valid Name",
                Description = "Valid Description",
                Type = "GuestHouse",
                HasInstantBooking = true,
                ContactEmail = invalidEmail,
                ContactNumber = "+123456789"
            };

            var validator = new UpdateAccommodationCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("+123")]
        [InlineData("abcdefg")]
        public void ValidatorForInvalidContactNumber_ShouldHaveValidationErrors(string invalidContactNumber)
        {
            // Arrange
            var command = new UpdateAccommodationCommand
            {
                Guid = Guid.NewGuid(),
                Name = "Valid Name",
                Description = "Valid Description",
                Type = "GuestHouse",
                HasInstantBooking = true,
                ContactEmail = "contact@valid.com",
                ContactNumber = invalidContactNumber
            };

            var validator = new UpdateAccommodationCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ContactNumber);
        }

    }
}
