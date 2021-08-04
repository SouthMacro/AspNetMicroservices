namespace DataAccessCore.Catalog.API.Context.MongoFacadeFunctions
{
    using MongoDB.Bson;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System;

    public abstract class TemplateFunction : ITemplateFunction
    {
        public ObjectId Id { get; set; }

        [Column(TypeName = "DateTime2")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // this will prevent the column to be updated
        public DateTime CreatedDate { get; internal set; }

        [Column(TypeName = "DateTime2")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime? UpdatedDate { get; set; }

        [Required]
        public bool? IsActive { get; set; }
    }
}
