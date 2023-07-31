using Core.Database.Interface;
using MongoDB.Driver;

namespace Core.Database.Context
{
    public class Context: IContext
    {
        private IMongoDatabase _mongoDatabase;

        public Context(IMongoClient mongoClient, string database)
        {
            _mongoDatabase = mongoClient.GetDatabase(database);
        }

        public IMongoCollection<T> DbMongoCollectionSet<T>(string collection)
        {
            return _mongoDatabase.GetCollection<T>(collection);
        }
    }
}