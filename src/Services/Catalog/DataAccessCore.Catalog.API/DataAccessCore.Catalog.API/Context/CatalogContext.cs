﻿namespace DataAccessCore.Catalog.API.Context
{
    using DataAccessCore.Catalog.API.Context.OtherContext;
    using DataAccessCore.Catalog.API.Entities;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;

    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
