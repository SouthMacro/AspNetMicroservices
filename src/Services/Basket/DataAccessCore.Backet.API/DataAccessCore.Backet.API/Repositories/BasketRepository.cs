using DataAccessCore.Backet.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace DataAccessCore.Backet.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;

        public Task DeleteBasketAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<ShoppingCart> GetBasketAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart)
        {
            throw new System.NotImplementedException();
        }
    }
}
