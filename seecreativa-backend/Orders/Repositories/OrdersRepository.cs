using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using seecreativa_backend.Clients.Repositories;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Orders.Entities;
using seecreativa_backend.Orders.Models;
using seecreativa_backend.Orders.Persistance;
using seecreativa_backend.Prices.Repositories;
using seecreativa_backend.Products.Repositories;

namespace seecreativa_backend.Orders.Repositories {
    public interface IOrdersRepository {
        public Task<Order> CreateAsync(OrderCreateDto createDto);
        public Task<IEnumerable<Order>> GetAllAsync();
        public Task<Order?> GetByIdAsync(string id);
        public Task<bool> DeleteByIdAsync(string id);
    }

    public class OrdersRepository : IOrdersRepository {
        private readonly IMongoCollection<Order> _collection;

        private readonly IClientsRepository _clientsRepository;
        private readonly IPricesRepository _pricesRepository;
        private readonly IProductsRepository _productsRepository;

        public OrdersRepository(
            IOptions<MongoDbSettings> settings, IClientsRepository clientsRepository,
            IPricesRepository pricesRepository,
            IProductsRepository productsRepository
        ) {
            _collection = new OrdersContext(settings).Orders;

            _clientsRepository = clientsRepository;
            _pricesRepository = pricesRepository;
            _productsRepository = productsRepository;
        }

        public async Task<Order> CreateAsync(OrderCreateDto createDto) {
            var order = createDto.ToEntity();
            var price = await _pricesRepository.GetByIdAsync(createDto.PriceId);
            foreach (var product in order.Products) {
                var foundProduct = await _productsRepository.GetByIdAsync(product.ProductId);
                product.Price = OrderProduct.GetPrice(price!, foundProduct!);
            }
            await _collection.InsertOneAsync(order);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllAsync() {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(string id) {
            var order = await _collection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
            if (order == null) return null;
            return order;
        }

        public async Task<bool> DeleteByIdAsync(string id) {
            var order = GetByIdAsync(id);
            if (order == null) return false;
            var filter = Builders<Order>.Filter.Eq(x => x.Id, GetObjectId(id));
            var result = await _collection.DeleteOneAsync(filter);
            if (result == null || result.DeletedCount == 0) return false;
            return true;
        }

        private static ObjectId GetObjectId(string id) {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;
            return internalId;
        }
    }
}
