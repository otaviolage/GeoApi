using AutoFixture;
using GeoApi.Domain.Interfaces.Repositories;
using GeoApi.Domain.Models.Countries;
using GeoApi.Domain.Services;
using Moq;

namespace GeoApi.Tests.Domain.Services
{
    public class CountriesServiceTest
    {
        private readonly Fixture _factory = new();

        [Fact]
        public void GetAll_ShouldGetAllCountriesFromRepository()
        {
            //Arrange
            var mockCountriesRepository = new Mock<IGeoDBRepository>();
            var countriesService = new CountriesService(mockCountriesRepository.Object);

            var countries = _factory.Create<CountriesModel>();
            mockCountriesRepository.Setup(x => x.GetAll(
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .ReturnsAsync(countries);

            //Act
            var result = countriesService.GetAll().Result;

            //Assert
            for (var i = 0; i < result.Data.Count(); i++)
            {
                Assert.Equal(countries.Data.ElementAt(i).Code, result.Data.ElementAt(i).Code);
                Assert.Equal(countries.Data.ElementAt(i).Name, result.Data.ElementAt(i).Name);

                for (var j = 0; j < result.Data.Count(); j++)
                    Assert.Equal(countries.Data.ElementAt(i).CurrencyCodes.ElementAt(j),
                        result.Data.ElementAt(i).CurrencyCodes.ElementAt(j));
            }

            Assert.Equal(countries.Metadata.CurrentOffset, result.Metadata.CurrentOffset);
            Assert.Equal(countries.Metadata.TotalCount, result.Metadata.TotalCount);

            mockCountriesRepository.Verify(x => x.GetAll(
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Once());
        }
    }
}
