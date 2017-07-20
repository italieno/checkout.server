using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Checkout.Server.Api.Responses;
using Checkout.Server.Core.Models.Api;
using Checkout.Server.Core.Models.Api.Response;

namespace Checkout.Server.Api.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var validationErrors = new List<ValidationErrorModel>(); ;

                foreach (var modelKeyValuePair in actionContext.ModelState)
                {
                    validationErrors.Add(new ValidationErrorModel()
                    {
                        Field = modelKeyValuePair.Key,
                        Messages = modelKeyValuePair.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    });
                }

                var error = new ApiErrorModel(HttpStatusCode.BadRequest, "inputdata validation error", validationErrors);
             
                actionContext.Response = BadResponseWithError.GetResponse(error);
            }
        }
    }
}