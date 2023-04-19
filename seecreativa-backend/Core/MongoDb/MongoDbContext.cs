using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace seecreativa_backend.Core.MongoDb
{
    public class MongoDbContext<T>
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        protected IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
