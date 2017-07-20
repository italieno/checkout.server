using System;
using Checkout.Server.Core.Enums;
using Checkout.Server.Core.Models.Shopping;

namespace Checkout.Server.Core.Models.Commands
{
    public class RemoveItemCommandModel : IShoppingCartCommandModel
    {
        public RemoveItemCommandModel(IShoppingItemModel item)
        {
            Id = Guid.NewGuid().ToString("N");
            Type = ShoppingCartCommandType.RemoveItem;
            Item = item;
        }

        public string Id { get; }
        public ShoppingCartCommandType Type { get; }
        public IShoppingItemModel Item { get; }
    }
}