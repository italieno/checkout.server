using System.Net.Http;
using System.Net.Http.Formatting;
using Checkout.Server.Core.Models;
using Checkout.Server.Core.Models.Api;
using Newtonsoft.Json.Serialization;

namespace Checkout.Server.Api.Responses
{
    public static class BadResponseWithError
    {
        public static HttpResponseMessage GetResponse(ApiErrorModel apiError)
        {
            var formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
            };

            return new HttpResponseMessage(apiError.HttpStatusCode)
            {
                Content = new ObjectContent<ApiErrorModel>(apiError, formatter, JsonMediaTypeFormatter.DefaultMediaType.MediaType)
            };
        }
    }
}
