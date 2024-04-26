using GeoApi.Domain.Interfaces.Repositories;
using GeoApi.Domain.Interfaces.Services;
using GeoApi.Domain.Models.Countries;

namespace GeoApi.Domain.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly IGeoDBRepository _countriesRepository;

        public CountriesService(IGeoDBRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public async Task<CountriesModel> GetAll()
        {
            var result = await _countriesRepository.GetAll();

            return result;
        }
    }
}