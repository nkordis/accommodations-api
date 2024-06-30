using Accommodations.Domain.Constants;
using FluentAssertions;
using Xunit;

namespace Accommodations.App.User.Tests
{
    public class CurrentUserTests
    {
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            // Arrange
            var currentUser = new CurrentUser("21B6BD26-B363-43BA-BCA4-898367AB452E", "test@test.com",
                [UserRoles.Admin, UserRoles.User], null, null);

            // Act
            var isInRole = currentUser.IsInRole(roleName);

            // Assert
            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            // Arrange
            var currentUser = new CurrentUser("21B6BD26-B363-43BA-BCA4-898367AB452E", "test@test.com",
                [UserRoles.Admin, UserRoles.User], null, null);

            // Act
            var isInRole = currentUser.IsInRole(UserRoles.Owner);

            // Assert
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            // Arrange
            var currentUser = new CurrentUser("21B6BD26-B363-43BA-BCA4-898367AB452E", "test@test.com",
                [UserRoles.Admin, UserRoles.User], null, null);

            // Act
            var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

            // Assert
            isInRole.Should().BeFalse();
        }
    }
}