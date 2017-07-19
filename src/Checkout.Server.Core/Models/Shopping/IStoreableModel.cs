using System;

namespace Checkout.Server.Core.Models.Shopping
{
    public interface IStoreableModel
    {
        IComparable Id { get; }
    }
}