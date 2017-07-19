using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Checkout.Server.Api.Responses;
using Checkout.Server.Core.Models;

namespace Checkout.Server.Api.Requests
{
    public class BadRequestWithError : IHttpActionResult
    {
        private readonly ErrorModel _error;

        public BadRequestWithError(ErrorModel error)
        {
            _error = error;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = BadResponseWithError.GetResponse(_error);
            return Task.FromResult(response);
        }
    }
}