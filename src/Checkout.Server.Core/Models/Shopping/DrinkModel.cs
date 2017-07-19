using System;
using Checkout.Server.Core.Enums;

namespace Checkout.Server.Core.Models.Shopping
{
    public class DrinkModel : IShoppingItemModel
    {
        public DrinkModel(string id, int quantity = 1)
        {
            Id = id;
            Quantity = quantity;
        }

        public int Quantity { get; }
        public ShoppingItemType ItemType => ShoppingItemType.Drink;
        public IComparable Id { get; }
    }
}