namespace Checkout.Server.Core.Models.Api.Inputs
{
    public class ShoppingCartItemInputModel : IShoppingCartItemInputModel
    {
        public string What { get; set; }
        public int HowMany { get; set; }
    }
}