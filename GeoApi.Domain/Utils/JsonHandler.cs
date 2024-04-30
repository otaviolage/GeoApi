using FluentResults;
using GeoApi.Domain.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace GeoApi.Domain.Utils
{
    public class JsonHandler
    {
        public static Result<T> DeserializeJson<T>(string model)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(model);
                return Result.Ok(result!);
            }
            catch (Exception)
            {
                return Result.Fail<T>(new ResultError(
                    "A external request result does not comply with the expected format or schema",
                    HttpStatusCode.UnprocessableContent));
            }
        }
    }
}
