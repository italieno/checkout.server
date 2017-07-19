using System;
using System.Collections.Generic;
using System.Net;

namespace Checkout.Server.Core.Models
{
    public class ErrorModel
    {
        public ErrorModel(HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, 
            string message = null, 
            List<ValidationErrorModel> validationErrors = null)
        {
            Id = Guid.NewGuid();
            HttpStatusCode = httpStatusCode;
            Message = message;
            ValidationErrors = validationErrors;
        }

        public Guid Id { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }
        public string Message { get; private set; }
        public List<ValidationErrorModel> ValidationErrors { get; private set; }
    }
}