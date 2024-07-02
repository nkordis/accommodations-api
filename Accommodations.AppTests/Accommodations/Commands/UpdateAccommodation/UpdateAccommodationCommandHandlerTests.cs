using Accommodations.Domain.Constants;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Exceptions;
using Accommodations.Domain.Interfaces;
using Accommodations.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Accommodations.App.Accommodations.Commands.UpdateAccommodation.Tests
{
    public class UpdateAccommodationCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateAccommodationCommandHandler>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAccommodationsRepository> _accommodationsRepositoryMock;
        private readonly Mock<IAccommodationAuthorizationService> _accommodationAuthorizationServiceMock;
        private readonly UpdateAccommodationCommandHandler _handler;

        public UpdateAccommodationCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateAccommodationCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _accommodationsRepositoryMock = new Mock<IAccommodationsRepository>();
            _accommodationAuthorizationServiceMock = new Mock<IAccommodationAuthorizationService>();
            _handler = new UpdateAccommodationCommandHandler(_loggerMock.Object, _mapperMock.Object, _accommodationsRepositoryMock.Object, _accommodationAuthorizationServiceMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
        {
            // Arrange
            var accommodationId = Guid.NewGuid();
            var command = new UpdateAccommodationCommand()
            {
                Guid = accommodationId
                
            };

            var existingAccommodation = new Accommodation()
            {
                Id = accommodationId  
            };

            _accommodationsRepositoryMock.Setup(repo => repo.GetAsync(accommodationId))
                .ReturnsAsync(existingAccommodation);

            _accommodationAuthorizationServiceMock.Setup(m => m.Authorize(existingAccommodation,
                ResourceOperation.Update)).Returns(true);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _accommodationsRepositoryMock.Verify(a => a.SaveChanges(), Times.Once);
            _mapperMock.Verify(m => m.Map(command, existingAccommodation), Times.Once);
        }

        [Fact()]
        public async Task Handle_WithNonExistingAccommodation_ShouldThrowNotFoundException()
        {
            // Arrange
            var accommodationId = Guid.NewGuid();
            var request = new UpdateAccommodationCommand()
            {
                Guid = accommodationId,
            };

            _accommodationsRepositoryMock.Setup(repo => repo.GetAsync(accommodationId))
                .ReturnsAsync((Accommodation?)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"{nameof(Accommodation)} with id: {accommodationId} doesn't exit.");
        }

        [Fact()]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {
            // Arrange
            var accommodationId = Guid.NewGuid();
            var request = new UpdateAccommodationCommand()
            {
                Guid = accommodationId

            };

            var existingAccommodation = new Accommodation()
            {
                Id = accommodationId
            };

            _accommodationsRepositoryMock.Setup(repo => repo.GetAsync(accommodationId))
                .ReturnsAsync(existingAccommodation);

            _accommodationAuthorizationServiceMock.Setup(m => m.Authorize(existingAccommodation,
                ResourceOperation.Update)).Returns(false);


            // Act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ForbidException>();
        }
    }
}