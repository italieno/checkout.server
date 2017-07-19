namespace Checkout.Server.Core.Models.Api
{
    public class SuccessReponseModel : BasicApiResponseModel
    {
        public SuccessReponseModel() : this(null)
        {
        }

        public SuccessReponseModel(object content)
        {
            IsSuccess = true;
            Content = content;
        }
    }
}