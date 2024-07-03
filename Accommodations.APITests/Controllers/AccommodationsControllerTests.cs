using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;

namespace Accommodations.API.Controllers.Tests
{
    public class AccommodationsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;

        public AccommodationsControllerTests(WebApplicationFactory<Program> applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }

        [Fact()]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();

            // Act
            var result = await client.GetAsync("/api/accommodations?pageNumber=1&pageSize=10");

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact()]
        public async Task GetAll_ForInvalidRequest_Returns400BadRequest()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();

            // Act
            var result = await client.GetAsync("/api/accommodations");

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}