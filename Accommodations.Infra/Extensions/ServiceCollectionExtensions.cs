﻿using Accommodations.Infra.Persistence;
using Accommodations.Infra.Seeders;
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
            services.AddDbContext<AccommodationsDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IAccommodationSeeder, AccommodationSeeder>();
        }
    }
}
