using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Products.Entities;

namespace seecreativa_backend.Products.Persistance {
    public class ProductsContext : MongoDbContext<Product> {
        public ProductsContext(IOptions<MongoDbSettings> settings) : base(settings) { }

        public IMongoCollection<Product> Products => GetCollection("products");
    }
}
