namespace DataAccessCore.Catalog.API.Context.MongoFacadeFunctions.Interfaces
{
using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IMongoContextFacade<TEntity> 
        where TEntity : ITemplateFunction
    {
        IQueryable<TEntity> AsQueryable();

        IEnumerable<TEntity> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TProjected>> projectionExpression);

        TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        TEntity FindById(string id);

        Task<TEntity> FindByIdAsync(string id);

        void InsertOne(TEntity entity);

        Task InsertOneAsync(TEntity entity);

        void ReplaceOne(TEntity entity);

        Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity replacement);

        void InsertMany(ICollection<TEntity> entitys);

        Task InsertManyAsync(ICollection<TEntity> entitys);

        void DeleteById(string id);

        Task DeleteByIdAsync(string id);

        void DeleteOne(Expression<Func<TEntity, bool>> filterExpression);

        Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter);

        void DeleteMany(Expression<Func<TEntity, bool>> filterExpression);

        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
    }
}
