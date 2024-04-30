using FluentResults;
using GeoApi.Domain.Enums;
using GeoApi.Domain.Exceptions;
using GeoApi.Domain.Utils;

namespace GeoApi.Infrastructure
{
    [Serializable]
    public class ValidateRequests
    {
        public static T Validate<T>(Result<string> httpResult)
        {
            if (httpResult.IsFailed)
                ThrowHttpError(httpResult);

            var result = JsonHandler.DeserializeJson<T>(httpResult.Value);

            if (result.IsFailed)
                ThrowSerializerError(result.Errors);

            return result.Value;
        }

        public static ResultError? GetDefaultError(List<IError> errors) =>
            errors.OfType<ResultError>().FirstOrDefault();

        public static void ThrowHttpError(Result<string> httpResult)
        {
            var httpError = GetDefaultError(httpResult.Errors);

            if (httpError is null)
                throw new InvalidOperationException("No HttpError found in the Result object.");

            throw new ErrorException((ErrorCode)httpError!.StatusCode, httpError.Message);
        }

        public static void ThrowSerializerError(List<IError> errors)
        {
            var errorFound = GetDefaultError(errors);

            throw new ErrorException((ErrorCode)errorFound!.StatusCode, errorFound.Message);
        }
    }
}