namespace DataAccessCore.Catalog.API.Repositories
{
    using DataAccessCore.Catalog.API.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICatalogRepository<TEntity>
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(string id);

        Task<IEnumerable<Product>> GetProductByNameAsync(string name);

        Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName);

        Task AddAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(string id);
    }
}
