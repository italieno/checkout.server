using System;
using System.Linq;
using Checkout.Server.Core.Models.Commands;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Dal.Repositories;

namespace Checkout.Server.Infra.Services.Commands
{
    public class ShoppingCartCommandService : IShoppingCartCommandService
    {
        private readonly IRepository<IShoppingItemModel> _itemsRepository;

        public ShoppingCartCommandService(IRepository<IShoppingItemModel> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public ICommandResponseModel RemoveItem(RemoveItemCommandModel commandModel)
        {
            if (string.IsNullOrEmpty(commandModel?.Item?.Id?.ToString()))
                throw new ArgumentException("Invalid RemoveItemCommand exception");

            var item = commandModel.Item;

            if (item.Quantity <= 0)
                return new ErrorCommandResponseModel($"wrong item quantity [{item.Quantity}]");

            try
            {
                var existingItem = _itemsRepository.FindById(item.Id);

                if (existingItem == null)
                    return new ErrorCommandResponseModel($"unable to find item [{item.Id}]");

                if (existingItem.Quantity < item.Quantity)
                    return new ErrorCommandResponseModel($"can't process request for [{item.Id}] [quantity:{existingItem.Quantity}] [requested:{item.Quantity}]");

                if (existingItem.Quantity == item.Quantity)
                    _itemsRepository.Delete(commandModel.Item.Id);

                if (existingItem.Quantity > item.Quantity)
                {
                    var newQuantity = existingItem.Quantity - item.Quantity;
                    _itemsRepository.Save(new ShoppingCartItemModel(item.Id.ToString(), newQuantity));
                }

                return new SuccessCommandResponseModel(true);
            }
            catch (Exception e)
            {
                return new ErrorCommandResponseModel(e);
            }
        }

        public ICommandResponseModel AddItem(AddItemCommandModel commandModel)
        {
            if (string.IsNullOrEmpty(commandModel?.Item?.Id?.ToString()))
                throw new ArgumentException("Invalid AddItemCommandModel exception");

            var item = commandModel.Item;

            if (item.Quantity <= 0)
                return new ErrorCommandResponseModel($"wrong item quantity [{item.Quantity}]");

            try
            {
                var existingItem = _itemsRepository.FindById(item.Id);

                if (existingItem == null)
                {
                    _itemsRepository.Save(item);
                }
                else
                {
                    var newQuantity = existingItem.Quantity + item.Quantity;
                    _itemsRepository.Save(new ShoppingCartItemModel(item.Id.ToString(), newQuantity));
                }

                return new SuccessCommandResponseModel(true);
            }
            catch (Exception e)
            {
                return new ErrorCommandResponseModel(e);
            }
        }

        public ICommandResponseModel UpdateItem(UpdateItemCommandModel commandModel)
        {
            if (string.IsNullOrEmpty(commandModel?.Item?.Id?.ToString()))
                throw new ArgumentException("Invalid UpdateItemCommandModel exception");

            var item = commandModel.Item;

            if (item.Quantity < 0)
                return new ErrorCommandResponseModel($"wrong item quantity [{item.Quantity}]");

            try
            {
                var existingItem = _itemsRepository.FindById(item.Id);

                if (item.Quantity == 0 && existingItem == null)
                {
                    //Do nothing
                }

                if (item.Quantity == 0 && existingItem != null)
                {
                    _itemsRepository.Delete(commandModel.Item.Id);
                }
                
                if (item.Quantity > 0)
                {
                    _itemsRepository.Save(item);
                }
                
                return new SuccessCommandResponseModel(true);
            }
            catch (Exception e)
            {
                return new ErrorCommandResponseModel(e);
            }
        }

        public ICommandResponseModel RemoveAll()
        {
            try
            {
                foreach (var item in  _itemsRepository.All())
                {
                    _itemsRepository.Delete(item.Id);
                }

                return new SuccessCommandResponseModel(true);
            }
            catch (Exception e)
            {
                return new ErrorCommandResponseModel(e);
            }
        }
    }
}