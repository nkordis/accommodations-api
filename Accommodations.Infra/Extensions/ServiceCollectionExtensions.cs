using Accommodations.Domain.Entities;
using Accommodations.Domain.Repositories;
using Accommodations.Infra.Persistence;
using Accommodations.Infra.Repositories;
using Accommodations.Infra.Seeders;
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
            services.AddDbContext<AccommodationsDbContext>(options => options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AccommodationsDbContext>();

            services.AddScoped<IAccommodationSeeder, AccommodationSeeder>();
            services.AddScoped<IAccommodationsRepository, AccommodationsRepository>();
            services.AddScoped<IUnitsRepository, UnitsRepository>();
        }
    }
}
