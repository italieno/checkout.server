namespace Checkout.Server.Core.Models
{
    public interface IResponseModel
    {
        bool IsSuccess { get; }
        object Content { get; }
    }
}