using Checkout.Server.Core.Models.Api;
using Checkout.Server.Core.Models.Api.Inputs;

namespace Checkout.Server.Infra.Services.Controllers
{
    public interface IShoppingCartControllerService
    {
        IApiResponseModel GetAll();
        IApiResponseModel GetItem(string id);
        IApiResponseModel RemoveItem(ShoppingCartItemInputModel input);
        IApiResponseModel AddItem(ShoppingCartItemInputModel input);
        IApiResponseModel UpdateItem(ShoppingCartItemInputModel input);
        IApiResponseModel RemoveAll();
    }
}