using System;
using Newtonsoft.Json;

namespace Checkout.Server.Core.Models.Shopping
{
    public interface IStoreableModel
    {
        [JsonProperty("id")]
        IComparable Id { get; }
    }
}