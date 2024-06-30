using FluentValidation.TestHelper;
using Xunit;

namespace Accommodations.App.Accommodations.Commands.CreateAccommodation.Tests
{
    public class CreateAccommodationCommandValidatorTests
    {
        [Fact()]
        public void ValidatorForValidCommand_SHouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new CreateAccommodationCommand()
            {
                Name = "Mountain View Cabin",
                Description = "A cozy cabin with a breathtaking view of the mountains.",
                Type = "GuestHouse",
                HasInstantBooking = true,
                ContactEmail = "contact@mountainviewcabin.com",
                ContactNumber = "+123456789",
                City = "Aspen",
                Street = "123 Mountain Road",
                PostalCode = "12345"
            };

            var validator = new CreateAccommodationCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ValidatorForInvalidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var command = new CreateAccommodationCommand()
            {
                Name = "",
                Description = "",
                Type = "InvalidType",
                HasInstantBooking = true,
                ContactEmail = "invalid-email",
                ContactNumber = "12345",
                City = new string('a', 101),
                Street = new string('a', 101),
                PostalCode = new string('a', 51)
            };

            var validator = new CreateAccommodationCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.Type);
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
            result.ShouldHaveValidationErrorFor(x => x.ContactNumber);
            result.ShouldHaveValidationErrorFor(x => x.City);
            result.ShouldHaveValidationErrorFor(x => x.Street);
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
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
            var command = new CreateAccommodationCommand()
            {
                Name = "Valid Name",
                Description = "Valid Description",
                Type = validType,
                HasInstantBooking = true,
                ContactEmail = "contact@valid.com",
                ContactNumber = "+123456789",
                City = "Valid City",
                Street = "Valid Street",
                PostalCode = "12345"
            };

            var validator = new CreateAccommodationCommandValidator();

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
            var command = new CreateAccommodationCommand()
            {
                Name = "Valid Name",
                Description = "Valid Description",
                Type = "GuestHouse",
                HasInstantBooking = true,
                ContactEmail = invalidEmail,
                ContactNumber = "+123456789",
                City = "Valid City",
                Street = "Valid Street",
                PostalCode = "12345"
            };

            var validator = new CreateAccommodationCommandValidator();

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
            var command = new CreateAccommodationCommand()
            {
                Name = "Valid Name",
                Description = "Valid Description",
                Type = "GuestHouse",
                HasInstantBooking = true,
                ContactEmail = "contact@valid.com",
                ContactNumber = invalidContactNumber,
                City = "Valid City",
                Street = "Valid Street",
                PostalCode = "12345"
            };

            var validator = new CreateAccommodationCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ContactNumber);
        }

    }
}