using GeoApi.Domain.Models.Countries;

namespace GeoApi.Domain.Interfaces.Services
{
    public interface ICountriesService
    {
        Task<CountriesModel> GetAll();
    }
}