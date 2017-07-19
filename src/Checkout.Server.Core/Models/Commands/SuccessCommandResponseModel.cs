namespace Checkout.Server.Core.Models.Commands
{
    public class SuccessCommandResponseModel : ICommandResponseModel
    {
        public SuccessCommandResponseModel(object content = null)
        {
            Content = content;
            IsSuccess = true;
        }

        public bool IsSuccess { get; }
        public object Content { get; }
        public CommandErrorModel Error { get; }
    }
}