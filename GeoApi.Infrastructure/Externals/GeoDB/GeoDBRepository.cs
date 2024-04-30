using GeoApi.Domain.Interfaces.Repositories;
using GeoApi.Domain.Interfaces.Services;
using GeoApi.Domain.Models.Countries;

namespace GeoApi.Infrastructure.Externals.GeoDB
{
    internal class GeoDBRepository : IGeoDBRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpService _httpService;

        public GeoDBRepository(
            IHttpClientFactory httpClientFactory,
            IHttpService clientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpService = clientFactory;
        }

        public async Task<CountriesModel> GetAll(int offset = 0, int limit = 10)
        {
            var client = _httpClientFactory.CreateClient("GeoDBClient");

            var requestUri = $"{client.BaseAddress}/countries?offset={offset}&limit={limit}";

            var requestMessage = _httpService.CreateRequestMessage(HttpMethod.Get, requestUri);

            var countriesList = await _httpService.SendRequestAsync(client, requestMessage);

            var result = ValidateRequests.Validate<CountriesModel>(countriesList);

            return result;
        }
    }
}
