namespace Checkout.Server.Core.Models.Commands
{
    public interface ICommandResponseModel : IResponseModel
    {
        CommandErrorModel Error { get; }
    }
}
