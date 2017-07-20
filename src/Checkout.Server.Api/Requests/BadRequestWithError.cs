using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Checkout.Server.Api.Responses;
using Checkout.Server.Core.Models;
using Checkout.Server.Core.Models.Api;
using Checkout.Server.Core.Models.Api.Response;

namespace Checkout.Server.Api.Requests
{
    public class BadRequestWithError : IHttpActionResult
    {
        private readonly ApiErrorModel _apiError;

        public BadRequestWithError(ApiErrorModel apiError)
        {
            _apiError = apiError;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = BadResponseWithError.GetResponse(_apiError);
            return Task.FromResult(response);
        }
    }
}