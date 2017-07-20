using System;
using System.Net;
using Checkout.Server.Core.Models.Api;
using Checkout.Server.Core.Models.Api.Inputs;
using Checkout.Server.Core.Models.Commands;
using Checkout.Server.Core.Models.Shopping;
using Checkout.Server.Infra.Services.Commands;
using Checkout.Server.Infra.Services.Queries;

namespace Checkout.Server.Infra.Services.Controllers
{
    public class ShoppingCartControllerService : IShoppingCartControllerService
    {
        private readonly IShoppingCartCommandService _commandService;
        private readonly IShoppingCartQueryService _queryService;

        public ShoppingCartControllerService(IShoppingCartQueryService queryService, 
            IShoppingCartCommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }
        
        public IApiResponseModel GetAll()
        {
            try
            {
                var result = _queryService.GetAll();
                return new SuccessReponseModel(result);
            }
            catch (Exception e)
            {
                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, e.Message));
            }
            
        }

        public IApiResponseModel GetItem(string id)
        {
            try
            {
                var result = _queryService.GetItem(id);
                return new SuccessReponseModel(result);
            }
            catch (Exception e)
            {
                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, e.Message));
            }
        }

        public IApiResponseModel RemoveItem(ShoppingCartItemInputModel input)
        {
            try
            {
                //todo: better to use command factory here
                var command = new RemoveItemCommandModel(new ShoppingCartItemModel(input.What, input.HowMany));
                var result = _commandService.RemoveItem(command);

                if (result.IsSuccess)
                {
                    return new SuccessReponseModel(result.Content);
                }
                
                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, result.Error.Message));
                
            }
            catch (Exception e)
            {
                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, e.Message));
            }
        }

        public IApiResponseModel AddItem(ShoppingCartItemInputModel input)
        {
            try
            {
                //todo: better to use command factory here
                var command = new AddItemCommandModel(new ShoppingCartItemModel(input.What, input.HowMany));
                var result = _commandService.AddItem(command);

                if (result.IsSuccess)
                {
                    return new SuccessReponseModel(result.Content);
                }

                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, result.Error.Message));

            }
            catch (Exception e)
            {
                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, e.Message));
            }
        }

        public IApiResponseModel UpdateItem(ShoppingCartItemInputModel input)
        {
            try
            {
                //todo: better to use command factory here
                var command = new UpdateItemCommandModel(new ShoppingCartItemModel(input.What, input.HowMany));
                var result = _commandService.UpdateItem(command);

                if (result.IsSuccess)
                {
                    return new SuccessReponseModel(result.Content);
                }

                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, result.Error.Message));

            }
            catch (Exception e)
            {
                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, e.Message));
            }
        }

        public IApiResponseModel RemoveAll()
        {
            try
            {
                //todo: better to use command factory here
                var result = _commandService.RemoveAll();

                if (result.IsSuccess)
                {
                    return new SuccessReponseModel(result.Content);
                }

                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, result.Error.Message));

            }
            catch (Exception e)
            {
                return new ErrorApiResponseModel(new ApiErrorModel(HttpStatusCode.BadRequest, e.Message));
            }
        }
    }
}