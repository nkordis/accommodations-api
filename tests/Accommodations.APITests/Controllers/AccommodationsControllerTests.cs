using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Accommodations.APITests;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Accommodations.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Accommodations.Domain.Entities;
using Accommodations.App.Accommodations.Dtos;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Accommodations.Infra.Seeders;

namespace Accommodations.API.Controllers.Tests
{
    public class AccommodationsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;
        private readonly Mock<IAccommodationsRepository> _mockAccommodationsRepository = new();
        private readonly Mock<IAccommodationSeeder> _mockAccommodationSeeder = new();
        public AccommodationsControllerTests(WebApplicationFactory<Program> applicationFactory)
        {
            _applicationFactory = applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Scoped(typeof(IAccommodationsRepository), 
                        _ => _mockAccommodationsRepository.Object));
                    services.Replace(ServiceDescriptor.Scoped(typeof(IAccommodationSeeder),
                        _ => _mockAccommodationSeeder.Object));
                });
            });
        }

        [Fact()]
        public async Task GetByGuid_ForExistingGuid_Returns200OK()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();
            var guid = Guid.NewGuid();
            var accommodation = new Accommodation()
            {
                Id = guid,
                Name = "Test",
                Description = "Test description",
                Type = AccommodationType.Other,
                HasInstantBooking = true,
            };
            _mockAccommodationsRepository.Setup(m => m.GetAsync(guid)).ReturnsAsync(accommodation);

            // Act
            var response = await client.GetAsync($"/api/accommodations/{guid}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var accommodationDto = JsonConvert.DeserializeObject<AccommodationDto>(jsonString, settings);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            accommodationDto.Should().NotBeNull();
            accommodationDto.Id.Should().Be(guid);
            accommodationDto.Name.Should().Be("Test");
        }

        [Fact()]
        public async Task GetByGuid_ForNonExistingGuid_Returns404NotFound()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();
            var guid = Guid.NewGuid();
            _mockAccommodationsRepository.Setup(m => m.GetAsync(guid)).ReturnsAsync((Accommodation?)null);

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