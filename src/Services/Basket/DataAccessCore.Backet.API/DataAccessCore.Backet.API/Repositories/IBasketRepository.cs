namespace DataAccessCore.Backet.API.Repositories
{
    using DataAccessCore.Backet.API.Entities;
    using System.Threading.Tasks;

    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName);

        Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart);

        Task DeleteBasketAsync(string userName);
    }
}
