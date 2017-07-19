using System;

namespace Checkout.Server.Core.Models.Commands
{
    public class ErrorCommandResponseModel : ICommandResponseModel
    {
        public ErrorCommandResponseModel(string message = null)
        {
            IsSuccess = false;
            Error = new CommandErrorModel(message);
        }

        public ErrorCommandResponseModel(Exception e)
        {
            IsSuccess = false;
            Error = new CommandErrorModel(e);
        }

        public bool IsSuccess { get; }
        public object Content { get; }
        public CommandErrorModel Error { get; }
    }
}