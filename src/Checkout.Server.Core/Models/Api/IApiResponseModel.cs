namespace Checkout.Server.Core.Models.Api
{
    public interface IApiResponseModel : IResponseModel
    {
        ApiErrorModel ApiError { get; set; }
    }
}