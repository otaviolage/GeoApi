using GeoApi.Domain.Enums;
using GeoApi.Domain.Exceptions;
using GeoApi.Domain.Interfaces.Services;

namespace GeoApi.Domain.Services
{
    internal class HttpService : IHttpService
    {
        public HttpRequestMessage CreateRequestMessage(
            HttpMethod method,
            string requestUri,
            IDictionary<string, string>? headers = null,
            string? token = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(requestUri),
                Method = method
            };

            if (headers != null)
                foreach (var header in headers)
                    requestMessage.Headers.Add(header.Key, header.Value);

            if (token is not null)
                requestMessage.Headers.Add("Authorization", token);

            return requestMessage;
        }

        public virtual async Task<string> SendRequestAsync(HttpClient client, HttpRequestMessage httpRequestMessage)
        {
            var result = await client.SendAsync(httpRequestMessage);

            var response = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
                throw new ErrorException((ErrorCode)result.StatusCode, response!);

            return response;
        }

        public virtual void SendRequest(HttpClient client, HttpRequestMessage httpRequestMessage) =>
            client.SendAsync(httpRequestMessage);
    }
}
