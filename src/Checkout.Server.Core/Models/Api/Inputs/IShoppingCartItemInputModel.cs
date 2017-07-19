using System;

namespace Checkout.Server.Core.Models.Api.Inputs
{
    public interface IShoppingCartItemInputModel
    {
        string What { get; set; }
        int HowMany { get; set; }
    }
}
