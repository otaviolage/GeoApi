using Moq;
using Moq.Protected;

namespace GeoApi.Tests.Mocks
{
    internal class HttpClientMock
    {
        public static Mock<HttpMessageHandler> _httpMessageHandlerMock = new();
        public static HttpClient ArrangeHttpClientMock(HttpResponseMessage responseMessage)
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            return new HttpClient(_httpMessageHandlerMock.Object);
        }

        public static void AssertHttpClientMock(HttpRequestMessage httpRequestMessage) =>
            _httpMessageHandlerMock
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(x =>
                        x == httpRequestMessage),
                    ItExpr.IsAny<CancellationToken>());
    }
}
