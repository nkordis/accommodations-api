using AutoMapper;
using Accommodations.Domain.Entities;
using FluentAssertions;
using Xunit;
using Accommodations.App.Accommodations.Commands.CreateAccommodation;
using Accommodations.App.Accommodations.Commands.UpdateAccommodation;

namespace Accommodations.App.Accommodations.Dtos.Tests
{
    public class AccommodationsProfileTests
    {
        private IMapper _mapper;

        public AccommodationsProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccommodationsProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void CreateMap_ForAccommodationToAccommodationDto_MapsCorrectly()
        {
            // Arrange
            var accommodation = new Accommodation
            {
                Id = Guid.NewGuid(),
                Name = "Mountain View Cabin",
                Description = "A cozy cabin with a breathtaking view of the mountains.",
                Type = AccommodationType.GuestHouse,
                HasInstantBooking = true,
                ContactEmail = "contact@mountainviewcabin.com",
                ContactNumber = "+123456789",
                Address = new Address
                {
                    City = "Aspen",
                    Street = "123 Mountain Road",
                    PostalCode = "12345"
                }
            };

            // Act
            var accommodationDto = _mapper.Map<AccommodationDto>(accommodation);

            // Assert
            accommodationDto.Should().NotBeNull();
            accommodationDto.Id.Should().Be(accommodation.Id);
            accommodationDto.Name.Should().Be(accommodation.Name);
            accommodationDto.Description.Should().Be(accommodation.Description);
            accommodationDto.Type.Should().Be(accommodation.Type);
            accommodationDto.HasInstantBooking.Should().Be(accommodation.HasInstantBooking);
            accommodationDto.City.Should().Be(accommodation.Address.City);
            accommodationDto.Street.Should().Be(accommodation.Address.Street);
            accommodationDto.PostalCode.Should().Be(accommodation.Address.PostalCode);
        }

        [Fact]
        public void CreateMap_ForCreateAccommodationCommandToAccommodation_MapsCorrectly()
        {
            // Arrange
            var command = new CreateAccommodationCommand
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

            // Act
            var accommodation = _mapper.Map<Accommodation>(command);

            // Assert
            accommodation.Should().NotBeNull();
            accommodation.Name.Should().Be(command.Name);
            accommodation.Description.Should().Be(command.Description);
            accommodation.Type.Should().Be(AccommodationType.GuestHouse);
            accommodation.HasInstantBooking.Should().Be(command.HasInstantBooking);
            accommodation.ContactEmail.Should().Be(command.ContactEmail);
            accommodation.ContactNumber.Should().Be(command.ContactNumber);
            accommodation.Address.City.Should().Be(command.City);
            accommodation.Address.Street.Should().Be(command.Street);
            accommodation.Address.PostalCode.Should().Be(command.PostalCode);
        }

        [Fact]
        public void CreateMap_ForUpdateAccommodationCommandToAccommodation_MapsCorrectly()
        {
            // Arrange
            var command = new UpdateAccommodationCommand
            {
                Guid = Guid.NewGuid(),
                Name = "Updated Mountain View Cabin",
                Description = "An updated description of the cabin.",
                Type = "Hostel",
                HasInstantBooking = false,
                ContactEmail = "updated@mountainviewcabin.com",
                ContactNumber = "+987654321"
            };

            var existingAccommodation = new Accommodation
            {
                Id = command.Guid,
                Name = "Mountain View Cabin",
                Description = "A cozy cabin with a breathtaking view of the mountains.",
                Type = AccommodationType.GuestHouse,
                HasInstantBooking = true,
                ContactEmail = "contact@mountainviewcabin.com",
                ContactNumber = "+123456789",
                Address = new Address
                {
                    City = "Aspen",
                    Street = "123 Mountain Road",
                    PostalCode = "12345"
                }
            };

            // Act
            var updatedAccommodation = _mapper.Map(command, existingAccommodation);

            // Assert
            updatedAccommodation.Should().NotBeNull();
            updatedAccommodation.Id.Should().Be(command.Guid);
            updatedAccommodation.Name.Should().Be(command.Name);
            updatedAccommodation.Description.Should().Be(command.Description);
            updatedAccommodation.Type.Should().Be(AccommodationType.Hostel);
            updatedAccommodation.HasInstantBooking.Should().Be(command.HasInstantBooking.Value);
            updatedAccommodation.ContactEmail.Should().Be(command.ContactEmail);
            updatedAccommodation.ContactNumber.Should().Be(command.ContactNumber);
        }

        [Fact]
        public void CreateMap_ForUpdateAccommodationCommandToAccommodation_WhenCommandHasNullValues_ShouldNotMapNullValues()
        {
            // Arrange
            var command = new UpdateAccommodationCommand
            {
                Guid = Guid.NewGuid(),
                Name = null,
                Description = null,
                Type = null,
                HasInstantBooking = null,
                ContactEmail = null,
                ContactNumber = null
            };

            var existingAccommodation = new Accommodation
            {
                Id = command.Guid,
                Name = "Mountain View Cabin",
                Description = "A cozy cabin with a breathtaking view of the mountains.",
                Type = AccommodationType.GuestHouse,
                HasInstantBooking = true,
                ContactEmail = "contact@mountainviewcabin.com",
                ContactNumber = "+123456789",
                Address = new Address
                {
                    City = "Aspen",
                    Street = "123 Mountain Road",
                    PostalCode = "12345"
                }
            };

            // Act
            var updatedAccommodation = _mapper.Map(command, existingAccommodation);

            // Assert
            updatedAccommodation.Should().NotBeNull();
            updatedAccommodation.Id.Should().Be(command.Guid);
            updatedAccommodation.Name.Should().Be(existingAccommodation.Name);
            updatedAccommodation.Description.Should().Be(existingAccommodation.Description);
            updatedAccommodation.Type.Should().Be(existingAccommodation.Type);
            updatedAccommodation.HasInstantBooking.Should().Be(existingAccommodation.HasInstantBooking);
            updatedAccommodation.ContactEmail.Should().Be(existingAccommodation.ContactEmail);
            updatedAccommodation.ContactNumber.Should().Be(existingAccommodation.ContactNumber);
        }
    }
}
