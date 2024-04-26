namespace GeoApi.Domain.Interfaces.Services
{
    public interface IHttpService
    {
        HttpRequestMessage CreateRequestMessage(
            HttpMethod method,
        string requestUri,
            IDictionary<string, string> headers = null,
            string? token = null);

        Task<string> SendRequestAsync(HttpClient client, HttpRequestMessage httpRequestMessage);
        void SendRequest(HttpClient client, HttpRequestMessage httpRequestMessage);
    }
}