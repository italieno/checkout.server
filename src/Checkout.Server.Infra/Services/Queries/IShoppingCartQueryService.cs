using System.Collections.Generic;
using Checkout.Server.Core.Models.Shopping;

namespace Checkout.Server.Infra.Services.Queries
{
    public interface IShoppingCartQueryService
    {
        IEnumerable<IShoppingItemModel> GetAll();
        IShoppingItemModel GetItem(string id);
    }
}