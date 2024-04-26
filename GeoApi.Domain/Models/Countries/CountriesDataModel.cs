namespace GeoApi.Domain.Models.Countries
{
    public class CountriesDataModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> CurrencyCodes { get; set; }
    }
}