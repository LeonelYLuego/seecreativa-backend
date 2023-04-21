using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Prices.Entity;

namespace seecreativa_backend.Prices.Persistance {
    public class PricesContext : MongoDbContext<Price> {
        public PricesContext(IOptions<MongoDbSettings> settings) : base(settings) { }

        public IMongoCollection<Price> Prices => GetCollection("prices");
    }
}
