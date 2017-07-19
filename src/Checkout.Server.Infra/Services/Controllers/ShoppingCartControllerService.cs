using System;
using System.Net;
using Checkout.Server.Core.Models.Api;
using Checkout.Server.Core.Models.Commands;
using Checkout.Server.Infra.Services.Commands;
using Checkout.Server.Infra.Services.Queries;

namespace Checkout.Server.Infra.Services.Controllers
{
    public class ShoppingCartControllerService : IShoppingCartControllerService
    {
        private readonly IShoppingCartCommandService _commandService;
        private readonly IShoppingCartQueryService _queryService;

        public ShoppingCartControllerService(IShoppingCartQueryService queryService, IShoppingCartCommandService commandService)
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

        public IApiResponseModel RemoveItem(RemoveItemCommandModel command)
        {
            try
            {
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

        public IApiResponseModel AddItem(AddItemCommandModel command)
        {
            try
            {
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

        public IApiResponseModel UpdateItem(UpdateItemCommandModel command)
        {
            try
            {
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
    }
}