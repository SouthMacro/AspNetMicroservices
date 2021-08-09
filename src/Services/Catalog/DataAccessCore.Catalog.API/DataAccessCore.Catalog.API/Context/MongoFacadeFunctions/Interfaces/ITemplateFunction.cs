namespace DataAccessCore.Catalog.API.Context.MongoFacadeFunctions.Interfaces
{
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson;
    using System;

    public interface ITemplateFunction
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedDate { get; }

        DateTime? UpdatedDate { get; set; }

        bool? IsActive { get; set; }
    }
}
