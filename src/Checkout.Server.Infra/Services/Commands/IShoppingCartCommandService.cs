using Checkout.Server.Core.Models.Commands;

namespace Checkout.Server.Infra.Services.Commands
{
    public interface IShoppingCartCommandService
    {
        ICommandResponseModel RemoveItem(RemoveItemCommandModel commandModel);
        ICommandResponseModel AddItem(AddItemCommandModel commandModel);
        ICommandResponseModel UpdateItem(UpdateItemCommandModel commandModel);
        ICommandResponseModel RemoveAll();
    }
}