using GeoApi.Domain.Interfaces.Repositories;
using GeoApi.Infrastructure.Externals.GeoDB;
using Microsoft.Extensions.DependencyInjection;

namespace GeoApi.Infrastructure.DependencyInjection
{
    public static class Extension
    {

        public static IServiceCollection AddDataContext(this IServiceCollection services)
        {
            services.AddScoped<IGeoDBRepository, GeoDBRepository>();

            return services;
        }
    }
}