using Checkout.Server.Core.Enums;
using Newtonsoft.Json;

namespace Checkout.Server.Core.Models.Shopping
{
    public interface IShoppingItemModel : IStoreableModel
    {
        [JsonProperty("itemType")]
        ShoppingItemType ItemType { get; }

        [JsonProperty("quantity")]
        int Quantity{ get; }
    }
}
