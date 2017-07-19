using System.ComponentModel;
using Checkout.Server.Core.Enums;

namespace Checkout.Server.Core.Models.Shopping
{
    public interface IShoppingItemModel : IStoreable
    {
        ShoppingItemType ItemType { get; }
        int Quantity{ get; }
    }
}
