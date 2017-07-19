using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Checkout.Server.Api.Requests;
using Checkout.Server.Core.Models;
using Checkout.Server.Core.Models.Api;
using Checkout.Server.Core.Models.Commands;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Infra.Services.Controllers;

namespace Checkout.Server.Api.Controllers
{
    [RoutePrefix("shoppingcart")]
    public class ShoppingCartController : ApiController
    {
        private readonly IShoppingCartControllerService _controllerService;

        public ShoppingCartController(IShoppingCartControllerService controllerService)
        {
            _controllerService = controllerService;
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<IShoppingItemModel>))]
        [Route("", Name = "list")]
        public IHttpActionResult GetAll()
        {
            var response = _controllerService.GetAll();

            if (response.IsSuccess)
                return Ok(response.Content);

            return new BadRequestWithError(response.ApiError);
        }

        [HttpGet]
        [ResponseType(typeof(IShoppingItemModel))]
        [Route("{itemId}", Name = "item")]
        public IHttpActionResult GetItem(string itemId)
        {
            var response = _controllerService.GetItem(itemId);

            if (response.IsSuccess)
                return Ok(response.Content);

            return new BadRequestWithError(response.ApiError);
        }

        [HttpPost]
        [ResponseType(typeof(IResponseModel))]
        [Route("addItem", Name = "add")]
        public IHttpActionResult AddItem(DrinkModel model)
        {
            var response = _controllerService.AddItem(new AddItemCommandModel(model));

            if (response.IsSuccess)
                return Ok(response.Content);

            return new BadRequestWithError(response.ApiError);
        }
    }
}
