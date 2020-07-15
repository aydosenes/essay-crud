using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Essay.MongoDb
{
    public class BaseCrudRepository<TModel> :
       BaseRepository<TModel>,
       IBaseCrudRepository<TModel>
       where TModel : BaseModel
    {
        protected BaseCrudRepository(string connectionString, string databaseName, string collectionName) : base(connectionString, databaseName, collectionName)
        {
        }

        public virtual async Task Delete(string id)
        {
            await collection.DeleteOneAsync(x => x.Id == id);
        }

        public virtual async Task DeleteMany(FilterDefinition<TModel> predicate)
        {
            await collection.DeleteManyAsync(predicate);
        }

        public virtual async Task<IEnumerable<TModel>> GetAll()
        {
            var result = await collection.FindAsync(x => true);
            return await result.ToListAsync();
        }
        public virtual async Task<IEnumerable<TModel>> GetAll(FilterDefinition<TModel> predicate)
        {
            var result = await collection.FindAsync(predicate);
            return await result.ToListAsync();
        }

        public virtual async Task<bool> IsThere(FilterDefinition<TModel> predicate)
        {
            var result = await collection.FindAsync(predicate) != null;
            return result;
        }

        public virtual async Task<TModel> GetBy(string id)
        {
            var result = await collection.FindAsync(x => true);
            return await result.FirstOrDefaultAsync();
        }
        public virtual async Task<TModel> GetBy(FilterDefinition<TModel> predicate)
        {
            var result = await collection.FindAsync(predicate);
            return await result.FirstOrDefaultAsync();
        }
        public virtual async Task<TModel> Insert(TModel model)
        {
            await collection.InsertOneAsync(model);

            return model;
        }
        public virtual async Task InsertMany(IEnumerable<TModel> model)
        {
            await collection.InsertManyAsync(model);
        }
        public virtual async Task Replace(TModel model)
        {
            await collection.ReplaceOneAsync(x => x.Id == model.Id, model);
        }

        public virtual async Task Update(FilterDefinition<TModel> predicate, UpdateDefinition<TModel> model)
        {
            await collection.UpdateOneAsync(predicate, model);

        }
    }
}
