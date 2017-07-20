namespace Checkout.Server.Dal.Stores
{
    public interface IStore<out T>
    {
        T LoadData();
    }
}