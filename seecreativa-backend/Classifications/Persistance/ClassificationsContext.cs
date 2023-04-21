using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seecreativa_backend.Classifications.Entity;
using seecreativa_backend.Core.MongoDb;

namespace seecreativa_backend.Classifications.Persistance {
    public class ClassificationsContext : MongoDbContext<Classification> {
        public ClassificationsContext(IOptions<MongoDbSettings> settings) : base(settings) { }

        public IMongoCollection<Classification> Classifications => GetCollection("classifications");
    }
}
