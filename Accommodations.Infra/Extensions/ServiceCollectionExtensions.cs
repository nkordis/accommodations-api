using Accommodations.Infra.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Accommodations.Infra.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AccommodationsDbContext>();
        }
    }
}
