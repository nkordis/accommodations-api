using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authorization.Policy;
using Accommodations.APITests;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Accommodations.API.Controllers.Tests
{
    public class AccommodationsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;

        public AccommodationsControllerTests(WebApplicationFactory<Program> applicationFactory)
        {
            _applicationFactory = applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            });
        }

        [Fact()]
        public async Task GetByGuid_ForNonExistingGuid_Returns404NotFound()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();
            var guid = Guid.NewGuid();

            // Act
            var result = await client.GetAsync($"/api/accommodations/{guid}");

            // Assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
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