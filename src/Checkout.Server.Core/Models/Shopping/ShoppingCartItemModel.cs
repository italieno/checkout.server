using System;
namespace Checkout.Server.Core.Models.Shopping
{
    public class ShoppingCartItemModel : IShoppingItemModel
    {
        public ShoppingCartItemModel(string id, int quantity = 1)
        {
            Id = id;
            Quantity = quantity;
        }

        public int Quantity { get; }
       public IComparable Id { get; }
    }
}