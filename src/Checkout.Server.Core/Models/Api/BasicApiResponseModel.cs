namespace Checkout.Server.Core.Models.Api
{
    public class BasicApiResponseModel : IApiResponseModel
    {
        public bool IsSuccess { get; set; }
        public object Content { get; set; }
        public ApiErrorModel ApiError { get; set; }
    }
}