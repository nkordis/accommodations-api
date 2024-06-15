using Accommodations.App.Accommodations;
using Microsoft.Extensions.DependencyInjection;

namespace Accommodations.App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAccommodationsService, AccommodationsService>();

            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
