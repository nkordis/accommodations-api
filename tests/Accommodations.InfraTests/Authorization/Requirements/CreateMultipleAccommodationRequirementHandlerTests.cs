using Accommodations.App.User;
using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Xunit;

namespace Accommodations.Infra.Authorization.Requirements.Tests
{
    public class CreateMultipleAccommodationRequirementHandlerTests
    {
        private readonly Mock<IUserContext> _userContextMock;
        private readonly Mock<IAccommodationsRepository> _accommodationsRepositoryMock;
        private readonly CurrentUser _currentUser;

        public CreateMultipleAccommodationRequirementHandlerTests()
        {
            _currentUser = new CurrentUser("user-id", "test@test.com", Array.Empty<string>(), null, null);
            _userContextMock = new Mock<IUserContext>();
            _userContextMock.Setup(m => m.GetCurrentUser()).Returns(_currentUser);

            _accommodationsRepositoryMock = new Mock<IAccommodationsRepository>();
        }

        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleAccommodations_ShouldSucceed()
        {
            // Arrange
            var accommodations = new List<Accommodation>
            {
                new() { OwnerId = _currentUser.Id },
                new() { OwnerId = _currentUser.Id },
                new() { OwnerId = "other-id" }
            };

            _accommodationsRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync(accommodations);

            var requirement = new CreateMultipleAccommodationRequirement(2);
            var handler = new CreateMultipleAccommodationRequirementHandler(_accommodationsRepositoryMock.Object, _userContextMock.Object);
            var context = new AuthorizationHandlerContext(new[] { requirement }, null, null);

            // Act
            await handler.HandleAsync(context);

            // Assert
            context.HasSucceeded.Should().BeTrue();
        }

        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotCreatedMultipleAccommodations_ShouldFail()
        {
            // Arrange
            var accommodations = new List<Accommodation>
            {
                new() { OwnerId = _currentUser.Id },
                new() { OwnerId = "other-id" }
            };

            _accommodationsRepositoryMock.Setup(m => m.GetAllAsync()).ReturnsAsync(accommodations);

            var requirement = new CreateMultipleAccommodationRequirement(2);
            var handler = new CreateMultipleAccommodationRequirementHandler(_accommodationsRepositoryMock.Object, _userContextMock.Object);
            var context = new AuthorizationHandlerContext(new[] { requirement }, null, null);

            // Act
            await handler.HandleAsync(context);

            // Assert
            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();
        }
    }
}