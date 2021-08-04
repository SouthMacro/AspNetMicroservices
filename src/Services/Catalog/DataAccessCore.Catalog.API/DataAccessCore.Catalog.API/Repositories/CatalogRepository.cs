namespace DataAccessCore.Catalog.API.Repositories
{
    using DataAccessCore.Catalog.API.Context;
    using DataAccessCore.Catalog.API.Context.MongoFacadeFunctions;
    using DataAccessCore.Catalog.API.Entities;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CatalogRepository<TEntity> : ICatalogRepository<TEntity>
        where TEntity : ITemplateFunction
    {
        private readonly ICatalogContext _catalogContext;
        private static IMongoCollection<TEntity> _collection;

        internal static Func<IMongoContextFacade<TEntity>> MongoFunction = () => new MongoContextFacade<TEntity>(_collection);

        public CatalogRepository(ICatalogContext catalogContext)
        {
            this._catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        public async Task AddAsync(TEntity entity)
        {
            IMongoContextFacade<TEntity> configurationBuilder = MongoFunction();
            await configurationBuilder.InsertOneAsync(entity);
        }

        // TODO: For JSON format need checking filter: a => a.Id == entity.Id <---- it isn`t delegate type (Facade review need)
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            IMongoContextFacade<TEntity> configurationBuilder = MongoFunction();
            var data = await configurationBuilder.ReplaceOneAsync(replacement: entity);

            return data.IsAcknowledged && data.ModifiedCount > 0;
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _catalogContext.Products.Find(product => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(product => product.Category, categoryName);

            return await _catalogContext.Products.Find(filterDefinition).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _catalogContext.Products.Find(product => product.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(product => product.ProductName, name);

            return await _catalogContext.Products.Find(filterDefinition).ToListAsync();
        }
    }
}
