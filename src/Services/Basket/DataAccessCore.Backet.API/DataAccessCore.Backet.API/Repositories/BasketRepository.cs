namespace DataAccessCore.Backet.API.Repositories
{
    using DataAccessCore.Backet.API.Entities;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;

    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }
        public async Task<ShoppingCart> GetBasketAsync(string userName)
        {
            var backet = await _distributedCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(backet))
            {
                throw new ArgumentException(string.Empty, nameof(backet));
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(backet);
        }

        public Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteBasketAsync(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}
