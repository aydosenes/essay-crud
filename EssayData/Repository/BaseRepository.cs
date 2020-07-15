using MongoDB.Driver;

namespace Essay.MongoDb
{
    public abstract class BaseRepository<TModel> where TModel : BaseModel
    {
        protected readonly IMongoClient mongoClient;
        protected readonly IMongoDatabase mongoDatabase;
        protected readonly IMongoCollection<TModel> collection;

        protected BaseRepository(string connectionString, string databaseName, string collectionName)
        {
            var mongoClient = new MongoClient(connectionString);
            mongoDatabase = mongoClient.GetDatabase(databaseName);
            collection = mongoDatabase.GetCollection<TModel>(collectionName);
        }
    }
}   