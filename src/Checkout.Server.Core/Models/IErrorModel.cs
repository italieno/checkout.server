using System;

namespace Checkout.Server.Core.Models
{
    public interface IErrorModel
    {
        string Id { get; }
        string Message { get; }
    }
}