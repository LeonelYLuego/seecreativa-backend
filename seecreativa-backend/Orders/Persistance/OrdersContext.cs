using Microsoft.Extensions.Options;
using MongoDB.Driver;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Orders.Entities;

namespace seecreativa_backend.Orders.Persistance {
    public class OrdersContext : MongoDbContext<Order> {
        public OrdersContext(IOptions<MongoDbSettings> settings) : base(settings) { }

        public IMongoCollection<Order> Orders => GetCollection("orders");
    }
}
