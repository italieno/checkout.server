using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

namespace Checkout.Server.Api.Responses
{
    public static class BadResponseWithContent
    {
        public static HttpResponseMessage GetResponse(object content, HttpStatusCode code)
        {
            var formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
            };

            return new HttpResponseMessage(code)
            {
                Content = new ObjectContent<object>(content, formatter, JsonMediaTypeFormatter.DefaultMediaType.MediaType)
            };
        }
    }
}
