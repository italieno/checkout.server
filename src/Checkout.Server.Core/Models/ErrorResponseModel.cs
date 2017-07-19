namespace Checkout.Server.Core.Models
{
    public class ErrorResponseModel : BasicResponseModel
    {
        public ErrorResponseModel(ErrorModel error)
        {
            IsSuccess = false;
            Error = error;
        }
    }
}