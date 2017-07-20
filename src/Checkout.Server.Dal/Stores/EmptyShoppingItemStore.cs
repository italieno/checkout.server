using System.Collections.Generic;
using System.Linq;
using Checkout.Server.Core.Models.Shopping;

namespace Checkout.Server.Dal.Stores
{
    public class EmptyShoppingItemStore : ISimpleShoppingItemStore
    {
        public IEnumerable<IShoppingItemModel> LoadData()
        {
           return Enumerable.Empty<IShoppingItemModel>();
        }
    }
}