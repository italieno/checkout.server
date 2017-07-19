namespace Checkout.Server.Core.Models.Api
{
    public class ErrorApiResponseModel : BasicApiResponseModel
    {
        public ErrorApiResponseModel(ApiErrorModel apiError)
        {
            IsSuccess = false;
            ApiError = apiError;
        }
    }
}