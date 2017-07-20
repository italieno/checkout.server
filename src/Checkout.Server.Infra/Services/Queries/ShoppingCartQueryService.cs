using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Dal.Repositories;

namespace Checkout.Server.Infra.Services.Queries
{
    public class ShoppingCartQueryService : IShoppingCartQueryService
    {
        private readonly IRepository<IShoppingItemModel> _itemsRepository;

        public ShoppingCartQueryService(IRepository<IShoppingItemModel> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public IEnumerable<IShoppingItemModel> GetAll()
        {
            var list = _itemsRepository.All();
            return list ?? Enumerable.Empty<IShoppingItemModel>();
        }

        public IShoppingItemModel GetItem(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("invalid item id");

            return _itemsRepository.FindById(id);
        }
    }
}