namespace Checkout.Server.Core.Models
{
    public class SuccessReponseModel : BasicResponseModel
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