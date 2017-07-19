using System;

namespace Checkout.Server.Core.Models.Commands
{
    public class CommandErrorModel : IErrorModel
    {
        public CommandErrorModel(string message)
        {
            Id = Guid.NewGuid().ToString("N");
            Message = message;
        }

        public CommandErrorModel(Exception e)
        {
            Id = Guid.NewGuid().ToString("N");
            Message = $"{e.Message}: {e.StackTrace}";
        }

        public string Id { get; }
        public string Message { get; }
    }
}