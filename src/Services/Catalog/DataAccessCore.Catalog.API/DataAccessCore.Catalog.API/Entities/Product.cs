namespace DataAccessCore.Catalog.API.Entities
{
    using DataAccessCore.Catalog.API.Context.MongoFacadeFunctions;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Product : TemplateFunction<string>
    {
        [BsonElement("ProductName")]
        public string ProductName { get; set; }

        public string Category { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ImageFile { get; set; }

        public decimal Price { get; set; }
    }
}
