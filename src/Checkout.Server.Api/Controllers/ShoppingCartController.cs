using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Checkout.Server.Api.Controllers
{
    [RoutePrefix("shoppingcart")]
    public class ShoppingCartController : ApiController
    {
        // GET: api/ShoppingList

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        [Route("sessions/{userid}", Name = "usersessions")]
        public async Task<IHttpActionResult> GetAccessSessions(string userId)
        {
            var response = await Task.FromResult(new string[] {"value1", "value2"});

            return Ok(response);

            //return new BadRequestWithError(response.Error);
        }
    }
}
