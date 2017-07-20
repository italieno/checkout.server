using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Dal.Stores;

namespace Checkout.Server.Dal.Repositories
{
    public class SimpleInMemoryShoppingItemsRepository : IRepository<IShoppingItemModel>
    {
        private IEnumerable<IShoppingItemModel> _data;

        public SimpleInMemoryShoppingItemsRepository(ISimpleShoppingItemStore store)
        {
            if (store == null)
                throw new ArgumentException("store value cannot be null for SimpleInMemoryShoppingItemsRepository");

            _data = store.LoadData() ?? Enumerable.Empty<IShoppingItemModel>();
        }

        public IEnumerable<IShoppingItemModel> All()
        {
            return _data;
        }

        public void Delete(IComparable id)
        {
            if (id == null)
                throw new ArgumentException("Delete - id value cannot be null");

            if (FindById(id) == null)
                throw new KeyNotFoundException();

            _data = _data.Where(s => !Equals(s.Id, id));
        }

        public void Save(IShoppingItemModel item)
        {
            if (item?.Id == null)
                throw new ArgumentException("Save - invalid ShoppingItemModel");

            if (FindById(item.Id) != null)
            {
                Delete(item.Id);
            }

            _data = _data.Concat(new List<IShoppingItemModel>() { item });
        }

        public IShoppingItemModel FindById(IComparable id)
        {
            if (id == null)
                throw new ArgumentException("FindById - id value cannot be null");

            return _data.FirstOrDefault(s => s.Id.Equals(id));
        }
    }
}