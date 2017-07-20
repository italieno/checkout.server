namespace Checkout.Server.Core.Models.Api.Request
{
    public class ShoppingCartItemInputModel : IShoppingCartItemInputModel
    {
        public string What { get; set; }
        public int HowMany { get; set; }
    }
}