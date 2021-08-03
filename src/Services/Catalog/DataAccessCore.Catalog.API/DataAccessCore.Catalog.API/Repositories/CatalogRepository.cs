namespace DataAccessCore.Catalog.API.Repositories
{
    using DataAccessCore.Catalog.API.Context;
    using DataAccessCore.Catalog.API.Entities;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CatalogRepository : ICatalogRepository
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly ICatalogContext _catalogContext;

        public CatalogRepository(ICatalogRepository catalogRepository, ICatalogContext catalogContext)
        {
            this._catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
            this._catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _catalogContext.Products.Find(product => true).ToListAsync();
        }

        public Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
