using GeoApi.Domain.Interfaces.Services;
using GeoApi.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoApi.Domain.DependencyInjection
{
    public static class Extension
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IHttpService, HttpService>();

            services.AddHttpClient("GeoDBClient", client =>
            {
                client.BaseAddress = new Uri(configuration["GeoDB:BaseUrl"]!);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", configuration["GeoDB:Key"]!);
            });

            return services;
        }
    }
}