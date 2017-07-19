using Checkout.Server.Core.Models.Api;
using Checkout.Server.Core.Models.Commands;

namespace Checkout.Server.Infra.Services.Controllers
{
    public interface IShoppingCartControllerService
    {
        IApiResponseModel GetAll();
        IApiResponseModel GetItem(string id);
        IApiResponseModel RemoveItem(RemoveItemCommandModel command);
        IApiResponseModel AddItem(AddItemCommandModel command);
        IApiResponseModel UpdateItem(UpdateItemCommandModel command);
    }
}