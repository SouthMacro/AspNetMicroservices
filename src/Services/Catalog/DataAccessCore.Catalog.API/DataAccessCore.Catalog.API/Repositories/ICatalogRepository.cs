namespace DataAccessCore.Catalog.API.Repositories
{
    using DataAccessCore.Catalog.API.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICatalogRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(string id);

        Task<IEnumerable<Product>> GetProductByNameAsync(string name);

        Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName);

        Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class;

        Task UpdateAsync<TEntity>(TEntity entity)
            where TEntity : class;

        Task<bool> DeleteAsync(string id);
    }
}
