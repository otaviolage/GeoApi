using GeoApi.Domain.Enums;
using System.Runtime.Serialization;

namespace GeoApi.Domain.Exceptions
{
    [Serializable]
    public class ErrorException : Exception
    {
        public ErrorCode Code { get; set; }

        public ErrorException()
        {
        }

        public ErrorException(ErrorCode code, string message)
            : base(message)
        {
            Code = code;
        }

        protected ErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}