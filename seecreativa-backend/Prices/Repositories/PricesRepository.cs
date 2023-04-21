using Microsoft.Extensions.Options;
using seecreativa_backend.Core;
using seecreativa_backend.Core.MongoDb;
using seecreativa_backend.Prices.Entity;
using seecreativa_backend.Prices.Models;
using seecreativa_backend.Prices.Persistance;

namespace seecreativa_backend.Prices.Repositories {
    public interface IPricesRepository : IRepository<Price, PriceCreateDto, PriceUpdateDto> {
    }

    public class PricesRepository : MongoDbRepository<Price, PriceCreateDto, PriceUpdateDto>, IPricesRepository {
        public PricesRepository(IOptions<MongoDbSettings> settings) : base(new PricesContext(settings).Prices) { }
    }
}
