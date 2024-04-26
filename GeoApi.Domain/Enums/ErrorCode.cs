using System.Net;

namespace GeoApi.Domain.Enums
{
    public enum ErrorCode
    {
        NotFound = HttpStatusCode.NotFound,
        BadRequest = HttpStatusCode.BadRequest,
        Unauthorized = HttpStatusCode.Unauthorized
    }
}