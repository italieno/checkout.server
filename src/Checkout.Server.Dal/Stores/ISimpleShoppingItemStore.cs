using System.Collections.Generic;
using Checkout.Server.Core.Models.Shopping;

namespace Checkout.Server.Dal.Stores
{
    public interface ISimpleShoppingItemStore : IStore<IEnumerable<IShoppingItemModel>>
    {

    }
}