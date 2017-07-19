using System.Net.Http;
using System.Net.Http.Formatting;
using Checkout.Server.Core.Models;
using Newtonsoft.Json.Serialization;

namespace Checkout.Server.Api.Responses
{
    public static class BadResponseWithError
    {
        public static HttpResponseMessage GetResponse(ErrorModel error)
        {
            var formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
            };

            return new HttpResponseMessage(error.HttpStatusCode)
            {
                Content = new ObjectContent<ErrorModel>(error, formatter, JsonMediaTypeFormatter.DefaultMediaType.MediaType)
            };
        }
    }
}
