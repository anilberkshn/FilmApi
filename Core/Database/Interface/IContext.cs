using MongoDB.Driver;

namespace Core.Database.Interface
{
    public interface IContext
    {
        public IMongoCollection<T> DbMongoCollectionSet<T>(string collection);
    }
}