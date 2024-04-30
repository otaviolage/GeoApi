using FluentResults;
using System.Net;

namespace GeoApi.Domain.Exceptions
{
    [Serializable]
    public class ResultError : Error
    {
        public int StatusCode { get; }

        public ResultError(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = (int)statusCode;
        }
    }
}