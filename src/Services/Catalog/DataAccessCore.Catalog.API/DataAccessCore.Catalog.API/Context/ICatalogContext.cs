namespace DataAccessCore.Catalog.API.Context
{
    using DataAccessCore.Catalog.API.Entities;
    using MongoDB.Driver;

    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
