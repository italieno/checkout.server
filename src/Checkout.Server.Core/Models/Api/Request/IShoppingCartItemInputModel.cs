namespace Checkout.Server.Core.Models.Api.Request
{
    public interface IShoppingCartItemInputModel
    {
        string What { get; set; }
        int HowMany { get; set; }
    }
}
