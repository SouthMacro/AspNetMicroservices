namespace DataAccessCore.Catalog.API.Context.MongoFacadeFunctions
{
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using DataAccessCore.Catalog.API.Context.MongoFacadeFunctions.Interfaces;

    public class MongoContextFacade<TEntity, TEntityType> : IMongoContextFacade<TEntity, TEntityType>
        where TEntity : ITemplateFunction<TEntityType>
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoContextFacade(IMongoCollection<TEntity> settings)
        {
            this._collection = settings;
        }

        private protected string GetCollectionName(Type entityType)
        {
            return ((BsonCollectionAttribute)entityType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<TEntity> FilterBy(
            Expression<Func<TEntity, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual void InsertOne(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public virtual Task InsertOneAsync(TEntity entity)
        {
            return Task.Run(() => _collection.InsertOneAsync(entity));
        }

        public void InsertMany(ICollection<TEntity> entitys)
        {
            _collection.InsertMany(entitys);
        }


        public virtual async Task InsertManyAsync(ICollection<TEntity> entitys)
        {
            await _collection.InsertManyAsync(entitys);
        }

        public void ReplaceOne(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            _collection.FindOneAndReplace(filter, entity);
        }

        public virtual async Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity replacement)
        {
            return await _collection.ReplaceOneAsync(filter, replacement);
        }

        public void DeleteOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter)
        {
            return this._collection.DeleteOneAsync(filter);
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }
    }
}
