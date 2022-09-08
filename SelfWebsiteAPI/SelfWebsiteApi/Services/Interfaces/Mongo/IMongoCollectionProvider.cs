using MongoDB.Driver;

namespace SelfWebsiteApi.Services.Interfaces.Mongo
{
    public interface IMongoCollectionProvider
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
