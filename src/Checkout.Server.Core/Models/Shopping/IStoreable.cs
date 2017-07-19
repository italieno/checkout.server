using System;

namespace Checkout.Server.Core.Models.Shopping
{
    public interface IStoreable
    {
        IComparable Id { get; }
    }
}