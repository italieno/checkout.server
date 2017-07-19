namespace Checkout.Server.Core.Models
{
    public interface IResponseModel
    {
        bool IsSuccess { get; set; }
        object Content { get; set; }
        ErrorModel Error { get; set; }
    }
}