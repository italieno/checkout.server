namespace Checkout.Server.Core.Models.Api.Response
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