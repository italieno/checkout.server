using System.Collections.Generic;

namespace Checkout.Server.Core.Models
{
    public class ValidationErrorModel
    {
        public string Field { get; set; }
        public List<string> Messages { get; set; }
    }
}