namespace DataAccessCore.Catalog.API.Repositories
{
    using DataAccessCore.Catalog.API.Context;
    using DataAccessCore.Catalog.API.Context.MongoFacadeFunctions.Interfaces;
    using DataAccessCore.Catalog.API.Entities;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CatalogRepository<TEntity> : ICatalogRepository<TEntity>
        where TEntity : ITemplateFunction
    {
        private readonly ICatalogContext _catalogContext;
        private static readonly IMongoContextFacade<TEntity> _collection;

        public CatalogRepository(ICatalogContext catalogContext)
        {
            this._catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }

        internal static Func<IMongoContextFacade<TEntity>> MongoFunc = () => _collection;
        IMongoContextFacade<TEntity> mongoBuilder = MongoFunc();

        public async Task AddAsync(TEntity entity)
        {
            await mongoBuilder.InsertOneAsync(entity);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);

            var data = await mongoBuilder.ReplaceOneAsync(filter: filter, replacement: entity);

            return data.IsAcknowledged && data.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            FilterDefinition<TEntity> filterDefinition = Builders<TEntity>.Filter.Eq(product => product.Id.ToString(), id);

            DeleteResult deleteResult = await mongoBuilder.DeleteOneAsync(filterDefinition);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
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
