using AutoFixture;
using GeoApi.Domain.Services;
using GeoApi.Tests.Mocks;
using System.Net;

namespace GeoApi.Tests.Domain.Services
{
    public class HttpServiceTest
    {
        private readonly Fixture _factory = new();

        [Fact]
        public void CreateRequestMessage_ShouldCreate_HttpRequestMessage()
        {
            //Arrange
            var request = new HttpService();

            var method = _factory.Create<HttpMethod>();
            var requestUri = _factory.Create<Uri>().ToString();
            var headers = _factory.Create<IDictionary<string, string>?>();
            var token = _factory.Create<string?>();

            //Act
            var result = request.CreateRequestMessage(method, requestUri, headers, token);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(method, result.Method);
            Assert.Equal(new Uri(requestUri), result.RequestUri);

            foreach (var key in headers!.Keys)
                Assert.Equal(headers[key], result.Headers.GetValues(key).First());

            Assert.True(result.Headers.Contains("Authorization"));
            Assert.Equal(token, result.Headers.GetValues("Authorization").First());
        }

        [Fact]
        public void CreateRequestMessage_ShouldCreateAHttpRequestMessage_WithoutHeaders()
        {
            //Arrange
            var httpService = new HttpService();

            var method = _factory.Create<HttpMethod>();
            var requestUri = _factory.Create<Uri>().ToString();

            //Act
            var result = httpService.CreateRequestMessage(method, requestUri);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(method, result.Method);
            Assert.Equal(new Uri(requestUri), result.RequestUri);
            Assert.Empty(result.Headers);
        }

        [Fact]
        public async Task SendRequestAsync_SuccessfulResponse_ReturnsContent()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Success!")
            };

            var httpClient = HttpClientMock.ArrangeHttpClientMock(responseMessage);

            var httpService = new HttpService();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://teste.com");

            // Act
            var result = await httpService.SendRequestAsync(httpClient, httpRequestMessage);

            // Assert
            Assert.Equal("Success!", result.Value);

            HttpClientMock.AssertHttpClientMock(httpRequestMessage);
        }

        [Fact]
        public async Task SendRequestAsync_UnsuccessfulResponse_ThrowsErrorException()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Invalid request")
            };

            var httpClient = HttpClientMock.ArrangeHttpClientMock(responseMessage);

            var httpService = new HttpService();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://teste.com");

            // Act
            var result = await httpService.SendRequestAsync(httpClient, httpRequestMessage);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("Invalid request", result.Errors[0].Message);

            HttpClientMock.AssertHttpClientMock(httpRequestMessage);
        }

        [Fact]
        public void SendRequest_SendWithoutResponse()
        {
            // Arrange
            var httpClient = HttpClientMock.ArrangeHttpClientMock(null);

            var httpService = new HttpService();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://teste.com");

            // Act
            httpService.SendRequest(httpClient, httpRequestMessage);

            // Assert
            HttpClientMock.AssertHttpClientMock(httpRequestMessage);
        }
    }
}