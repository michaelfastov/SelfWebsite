using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SelfWebsiteApi.Database;
using SelfWebsiteApi.Services.Interfaces.Mongo;

namespace SelfWebsiteApi.Services.Implementations.Mongo
{
    public class MongoCollectionProvider: IMongoCollectionProvider
    {
        private readonly IMongoDatabase _mongoDb;

        public MongoCollectionProvider(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(
                mongoDbSettings.Value.ConnectionString);

            _mongoDb = mongoClient.GetDatabase(
                mongoDbSettings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _mongoDb.GetCollection<T>(collectionName);
        }
    }
}