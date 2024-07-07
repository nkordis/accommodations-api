using Accommodations.Domain.Entities;
using Accommodations.Domain.Interfaces;
using Accommodations.Domain.Repositories;
using Accommodations.Infra.Authorization;
using Accommodations.Infra.Authorization.Requirements;
using Accommodations.Infra.Authorization.Services;
using Accommodations.Infra.Configuration;
using Accommodations.Infra.Persistence;
using Accommodations.Infra.Repositories;
using Accommodations.Infra.Seeders;
using Accommodations.Infra.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accommodations.Infra.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AccommodationsDb");
            services.AddDbContext<AccommodationsDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null))
                .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<AccommodationsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<AccommodationsDbContext>();

            services.AddScoped<IAccommodationSeeder, AccommodationSeeder>();
            services.AddScoped<IAccommodationsRepository, AccommodationsRepository>();
            services.AddScoped<IUnitsRepository, UnitsRepository>();

            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality,
                    builder => builder.RequireClaim(AppClaimTypes.Nationality))
                .AddPolicy(PolicyNames.AtLeast18,
                    builder => builder.AddRequirements(new MinimumAgeRequirement(18)))
                .AddPolicy(PolicyNames.CreatedAtLeast2Accommodations,
                    builder => builder.AddRequirements(new CreateMultipleAccommodationRequirement(2)));


            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreateMultipleAccommodationRequirementHandler>();
            services.AddScoped<IAccommodationAuthorizationService, AccommodationAuthorizationService>();

            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
            services.AddScoped<IBlobStorageService, BlobStorageService>();
        }
    }
}
