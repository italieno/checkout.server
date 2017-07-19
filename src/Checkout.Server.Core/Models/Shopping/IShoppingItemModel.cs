using Checkout.Server.Core.Enums;

namespace Checkout.Server.Core.Models.Shopping
{
    public interface IShoppingItemModel : IStoreableModel
    {
        ShoppingItemType ItemType { get; }
        int Quantity{ get; }
    }
}
