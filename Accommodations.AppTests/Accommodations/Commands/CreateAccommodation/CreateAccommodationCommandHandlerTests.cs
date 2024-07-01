using Accommodations.App.User;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Accommodations.App.Accommodations.Commands.CreateAccommodation.Tests
{
    public class CreateAccommodationCommandHandlerTests
    {
        private readonly Mock<ILogger<CreateAccommodationCommandHandler>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAccommodationsRepository> _accommodationRepositoryMock;
        private readonly Mock<IUserContext> _userContextMock;
        private readonly CreateAccommodationCommandHandler _commandHandler;
        private readonly Guid _accommodationId;
        private readonly string _ownerId;
        private readonly string _userEmail;
        private readonly CreateAccommodationCommand _command;
        private readonly Accommodation _accommodation;

        public CreateAccommodationCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<CreateAccommodationCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _accommodationRepositoryMock = new Mock<IAccommodationsRepository>();
            _userContextMock = new Mock<IUserContext>();
            _commandHandler = new CreateAccommodationCommandHandler(_loggerMock.Object, _mapperMock.Object, _accommodationRepositoryMock.Object, _userContextMock.Object);

            _accommodationId = Guid.NewGuid();
            _ownerId = "owner-id";
            _userEmail = "test@test.com";
            _command = new CreateAccommodationCommand();
            _accommodation = new Accommodation();

            _mapperMock.Setup(m => m.Map<Accommodation>(_command)).Returns(_accommodation);
            _userContextMock.Setup(u => u.GetCurrentUser()).Returns(new CurrentUser(_ownerId, _userEmail, new List<string>(), null, null));
        }

        [Fact()]
        public async void Handle_ForValidCommand_ReturnsCreatedAccommodationId()
        {
            // Arrange
            _accommodationRepositoryMock.Setup(repo => repo.Create(It.IsAny<Accommodation>()))
                                                            .ReturnsAsync(_accommodationId);

            // Act
            var result = await _commandHandler.Handle(_command, CancellationToken.None);

            // Assert
            result.Should().Be(_accommodationId);
            _accommodation.OwnerId.Should().Be(_ownerId);
            _accommodationRepositoryMock.Verify(a => a.Create(_accommodation), Times.Once);
        }
    }
}