using System;
using System.Collections.Generic;
using System.Net;

namespace Checkout.Server.Core.Models.Api.Response
{
    public class ApiErrorModel : IErrorModel
    {
        public ApiErrorModel(HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, 
            string message = null, 
            List<ValidationErrorModel> validationErrors = null)
        {
            Id = Guid.NewGuid().ToString("N");
            HttpStatusCode = httpStatusCode;
            Message = message;
            ValidationErrors = validationErrors;
        }

        public string Id { get; }
        public HttpStatusCode HttpStatusCode { get; }
        public string Message { get; }
        public List<ValidationErrorModel> ValidationErrors { get; }
    }
}