namespace GeoApi.Domain.Models.Countries
{
    public class CountriesModel
    {
        public IEnumerable<CountriesDataModel> Data { get; set; }
        public CountriesMetadataModel Metadata { get; set; }
    }
}