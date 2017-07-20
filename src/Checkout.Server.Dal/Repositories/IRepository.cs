using System;
using System.Collections.Generic;
using Checkout.Server.Core.Models.Shopping;

namespace Checkout.Server.Dal.Repositories
{
    public interface IRepository<T> where T : IStoreableModel
    {
        IEnumerable<T> All();
        void Delete(IComparable id); 
        void Save(T item);
        T FindById(IComparable id);
    }
}