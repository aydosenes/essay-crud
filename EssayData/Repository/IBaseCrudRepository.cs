using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Essay.MongoDb
{
    public interface IBaseCrudRepository<TModel> where TModel : BaseModel
    {
        Task Delete(string id);
        Task DeleteMany(FilterDefinition<TModel> predicate);
        Task<IEnumerable<TModel>> GetAll();
        Task<IEnumerable<TModel>> GetAll(FilterDefinition<TModel> predicate);
        Task<bool> IsThere(FilterDefinition<TModel> predicate);
        Task<TModel> GetBy(string id);
        Task<TModel> GetBy(FilterDefinition<TModel> predicate);
        Task<TModel> Insert(TModel model);
        Task InsertMany(IEnumerable<TModel> model);
        Task Replace(TModel model);
        Task Update(FilterDefinition<TModel> predicate, UpdateDefinition<TModel> model);

    }
}
