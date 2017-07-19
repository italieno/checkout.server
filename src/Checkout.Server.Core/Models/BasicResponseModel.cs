namespace Checkout.Server.Core.Models
{
    public class BasicResponseModel : IResponseModel
    {
        public bool IsSuccess { get; set; }
        public object Content { get; set; }
        public ErrorModel Error { get; set; }
    }
}