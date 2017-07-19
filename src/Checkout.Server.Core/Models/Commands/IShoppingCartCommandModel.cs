using Checkout.Server.Core.Enums;
using Checkout.Server.Core.Models.Shopping;

namespace Checkout.Server.Core.Models.Commands
{
    public interface IShoppingCartCommandModel
    {
        string Id { get; }
        ShoppingCartCommandType Type { get; }
        IShoppingItemModel Item { get; }
    }
}