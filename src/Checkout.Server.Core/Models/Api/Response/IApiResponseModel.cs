namespace Checkout.Server.Core.Models.Api.Response
{
    public interface IApiResponseModel : IResponseModel
    {
        ApiErrorModel ApiError { get; set; }
    }
}