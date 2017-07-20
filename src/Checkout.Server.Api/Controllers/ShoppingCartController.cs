using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Checkout.Server.Api.Requests;
using Checkout.Server.Core.Models;
using Checkout.Server.Core.Models.Api.Inputs;
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
        [Authorize(Roles = "drink-manager")]
        [ResponseType(typeof(IResponseModel))]
        [Route("add", Name = "add")]
        public IHttpActionResult AddItem(ShoppingCartItemInputModel input)
        {
            var response = _controllerService.AddItem(input);

            if (response.IsSuccess)
                return Ok(response.Content);

            return new BadRequestWithError(response.ApiError);
        }

        [HttpPut]
        [Authorize(Roles = "drink-manager")]
        [ResponseType(typeof(IResponseModel))]
        [Route("update", Name = "update")]
        public IHttpActionResult Update(ShoppingCartItemInputModel input)
        {
            var response = _controllerService.UpdateItem(input);

            if (response.IsSuccess)
                return Ok(response.Content);

            return new BadRequestWithError(response.ApiError);
        }

        [HttpDelete]
        [Authorize(Roles = "drink-manager")]
        [ResponseType(typeof(IResponseModel))]
        [Route("remove", Name = "remove")]
        public IHttpActionResult Delete(ShoppingCartItemInputModel input)
        {
            var response = _controllerService.RemoveItem(input);

            if (response.IsSuccess)
                return Ok(response.Content);

            return new BadRequestWithError(response.ApiError);
        }

        //This endpoint has been developed to demo the "oauth client_credential" feature
        [HttpDelete]
        [Authorize(Roles = "trusted-app")]
        [ResponseType(typeof(IResponseModel))]
        [Route("reset", Name = "reset")]
        public IHttpActionResult Reset()
        {
            var response = _controllerService.RemoveAll();

            if (response.IsSuccess)
                return Ok(response.Content);

            return new BadRequestWithError(response.ApiError);
        }
    }
}
