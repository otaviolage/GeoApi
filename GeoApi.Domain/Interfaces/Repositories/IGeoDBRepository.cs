using GeoApi.Domain.Models.Countries;

namespace GeoApi.Domain.Interfaces.Repositories
{
    public interface IGeoDBRepository
    {
        Task<CountriesModel> GetAll(int offset = 0, int limit = 10);
    }
}