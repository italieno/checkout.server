using Newtonsoft.Json;

namespace Checkout.Server.Core.Models.Shopping
{
    public interface IShoppingItemModel : IStoreableModel
    {
        [JsonProperty("quantity")]
        int Quantity{ get; }
    }
}
