using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Checkout.Server.Api.Responses;

namespace Checkout.Server.Api.Requests
{
    public class BadRequestWithContent : IHttpActionResult
    {
        private readonly object _content;
        private readonly HttpStatusCode _code;

        public BadRequestWithContent(object content, HttpStatusCode code)
        {
            _content = content;
            _code = code;
        }

        public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = BadResponseWithContent.GetResponse(_content, _code);
            return Task.FromResult(response);
        }
    }
}
